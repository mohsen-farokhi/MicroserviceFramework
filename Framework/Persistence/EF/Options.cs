namespace Framework.Persistence.EF
{
	public class Options : object
	{
		public Enums.Provider Provider { get; set; }

		public string ConnectionString { get; set; }

		public string InMemoryDatabaseName { get; set; }
	}
}
