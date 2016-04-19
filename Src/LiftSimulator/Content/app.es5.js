'use strict';

(function () {

    var app = angular.module('lift-simulator', [], function ($httpProvider) {
        // Use x-www-form-urlencoded Content-Type
        $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
        var param = function param(obj) {
            var query = '',
                name,
                value,
                fullSubName,
                subName,
                subValue,
                innerObj,
                i;

            for (name in obj) {
                value = obj[name];

                if (value instanceof Array) {
                    for (i = 0; i < value.length; ++i) {
                        subValue = value[i];
                        fullSubName = name + '[' + i + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value instanceof Object) {
                    for (subName in value) {
                        subValue = value[subName];
                        fullSubName = name + '[' + subName + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value !== undefined && value !== null) query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
            }

            return query.length ? query.substr(0, query.length - 1) : query;
        };

        // Override $http service's default transformRequest
        $httpProvider.defaults.transformRequest = [function (data) {
            return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
        }];
    });

    app.controller = app.controller('lift-controller', ['$http', '$scope', function ($http, $scope) {

        $scope.tick = 0;
        $scope.levelCount = 10;
        $scope.levels = [];
        $scope.lifts = [];
        $scope.summaryItems = [];

        // Initialise the list of levels
        for (var idx = 1; idx <= $scope.levelCount; idx++) {
            var targetLevels = [];
            for (var targetIdx = 1; targetIdx <= $scope.levelCount; targetIdx++) {
                targetLevels.push(targetIdx);
            }
            $scope.levels.push({
                number: idx,
                waitingCount: 0,
                peopleCount: 0,
                targetLevel: 1,
                targetLevels: targetLevels
            });
        }

        $scope.advance = function () {
            $scope.tick++;
            $http.get('/api/v1/simulator/ticks/' + $scope.tick).success(function (response) {
                console.log(response);
                $scope.lifts = response.Context.Lifts;
                if (response.SummaryItems && response.SummaryItems.length) {
                    _.each(response.SummaryItems, function (summaryItem) {
                        $scope.summaryItems.push(summaryItem);
                    });
                }
                _.each(response.Context.Levels, function (responseLevel) {
                    var level = _.find($scope.levels, function (l) {
                        return l.number && l.number === responseLevel.Number;
                    });
                    if (level) {
                        level.waitingCount = responseLevel.Waiting;
                    }
                });
            });
        };

        $scope.reset = function () {
            $http.get('/api/v1/simulator/reset/').success(function (response) {
                console.log(response);
                $scope.lifts = response.Context.Lifts;
                $scope.tick = 0;
                _.each(response.Context.Levels, function (responseLevel) {
                    var level = _.find($scope.levels, function (l) {
                        return l.number && l.number === responseLevel.Number;
                    });
                    if (level) {
                        level.waitingCount = responseLevel.Waiting;
                    }
                });
            });
        };

        $scope.getLiftCssClass = function (liftId, level) {
            var lift = _.find($scope.lifts, function (l) {
                return l.Id && l.Id === liftId;
            });
            if (lift) {
                if (lift.CurrentFloor === level) {
                    return {
                        active: true,
                        up: lift.Direction === 0,
                        down: lift.Direction === 1,
                        stopped: lift.Direction === null
                    };
                }
            }
            return {};
        };

        $scope.callLift = function (peopleCount, sourceFloorNumber, targetFloorNumber) {
            if (peopleCount <= 0 || sourceFloorNumber < 1 || targetFloorNumber < 1) {
                return;
            }
            var level = _.find($scope.levels, function (l) {
                return l.number === sourceFloorNumber;
            });
            if (!level) {
                return;
            }
            var nPeopleCount = parseInt(peopleCount, 10);
            if (nPeopleCount !== NaN) {
                $http.post('api/v1/simulator/request-lift', {
                    tick: $scope.tick,
                    peopleCount: peopleCount,
                    sourceFloorNumber: sourceFloorNumber,
                    targetFloorNumber: targetFloorNumber
                }).success(function (response) {
                    level.peopleCount = 0;
                    level.waitingCount += nPeopleCount;
                    console.log(response);
                });
            }
        };

        $scope.reset();
    }]);

    app.filter('reverse', function () {
        return function (items) {
            return items.slice().reverse();
        };
    });
})();

