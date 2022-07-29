using System.Runtime.Serialization;

namespace razorWebApp.Utils;

public class AppException : Exception
{
	public int statusCode { get; }
	public AppException()
	{
	}

	public AppException(string message) : base(message)
	{
	}

	public AppException(string message, Exception innerException) : base(message, innerException)
	{
	}

	protected AppException(SerializationInfo info, StreamingContext context) : base(info, context)
	{
	}

	public AppException(string message, int statusCode) : this(message)
	{
		this.statusCode = statusCode;
	}
}