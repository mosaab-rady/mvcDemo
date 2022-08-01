namespace mvcApp.Configurations;

public class PostgresDbSettings
{
	public string Host { get; set; }
	public string Username { get; set; }
	public string Database { get; set; }
	public string Password { get; set; }
	public string ConnectionString
	{
		get
		{
			return $"Host={Host};Database={Database};Username={Username};Password={Password}";
		}
	}
}