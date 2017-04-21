using System;
using System.Collections.Generic;
using System.Linq;
using Elmah.Io.Client;
using log4net.Appender;
using log4net.helpers;
using log4net.spi;

namespace Elmah.io.Sitecore.log4net
{
	public class ElmahIoAppender : AppenderSkeleton
	{
		public Elmah.Io.Client.ILogger Logger;
		private Guid _logId;

		public string LogId
		{
			set
			{
				if (!Guid.TryParse(value, out _logId))
					throw new ArgumentException("LogId is not a GUID");
			}
		}

		public string Application { get; set; }

		protected override void Append(LoggingEvent loggingEvent)
		{
			if (Logger == null)
				Logger = new Elmah.Io.Client.Logger(_logId);

			var data = PropertiesToData(loggingEvent.Properties);

			var message = new Message(loggingEvent.RenderedMessage)
			{
				Severity = LevelToSeverity(loggingEvent.Level),
				DateTime = loggingEvent.TimeStamp.ToUniversalTime(),
				Detail = loggingEvent.GetExceptionStrRep(),
				Data = data,
				Application = Application ?? loggingEvent.Domain,
				Source = loggingEvent.LoggerName,
				User = loggingEvent.UserName
			};

			Logger.Log(message);
		}

		private static List<Item> PropertiesToData(PropertiesCollection properties)
		{
			return properties
				.GetKeys()
				.Select(key => new Item { Key = key, Value = properties[key].ToString() })
				.ToList();
		}

		private static Severity? LevelToSeverity(Level level)
		{
			if (level == Level.EMERGENCY)
				return Severity.Fatal;
			if (level == Level.FATAL)
				return Severity.Fatal;
			if (level == Level.ALERT)
				return Severity.Fatal;
			if (level == Level.CRITICAL)
				return Severity.Fatal;
			if (level == Level.SEVERE)
				return Severity.Fatal;
			if (level == Level.ERROR)
				return Severity.Error;
			if (level == Level.WARN)
				return Severity.Warning;
			if (level == Level.NOTICE)
				return Severity.Information;
			if (level == Level.INFO)
				return Severity.Information;
			if (level == Level.DEBUG)
				return Severity.Debug;
			if (level == Level.FINE)
				return Severity.Verbose;
			if (level == Level.TRACE)
				return Severity.Verbose;
			if (level == Level.FINER)
				return Severity.Verbose;
			if (level == Level.VERBOSE)
				return Severity.Verbose;
			if (level == Level.FINEST)
				return Severity.Verbose;

			return Severity.Information;
		}
	}
}
