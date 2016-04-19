using System;
using System.Data.SqlClient;

namespace LiftSimulator.Core
{
    public class Repository : IRepository
    {
        private readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddSummaryItem(SummaryItem item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO [dbo].[LiftMovements] " +
                                             "(Tick, LiftId, PeopleCount, [Level], Action, ActionDesc, Message) " +
                                             "VALUES " +
                                             "(@tick, @liftId, @peopleCount, @level, @action, @actionDesc, @message)", connection);

                command.Parameters.AddWithValue("@tick", item.Tick);
                command.Parameters.AddWithValue("@liftId", item.LiftId);
                command.Parameters.AddWithValue("@peopleCount", item.PeopleCount);
                command.Parameters.AddWithValue("@level", item.Level);
                command.Parameters.AddWithValue("@action", item.Action);
                command.Parameters.AddWithValue("@actionDesc", item.ActionDesc);
                command.Parameters.AddWithValue("@message", item.Message);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ResetSummaryItems()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM [dbo].[LiftMovements]", connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}