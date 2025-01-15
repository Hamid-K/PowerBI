using System;
using System.Collections.Generic;
using System.ComponentModel;
using NLog.Attributes;

namespace NLog
{
	// Token: 0x02000010 RID: 16
	[TypeConverter(typeof(LogLevelTypeConverter))]
	public sealed class LogLevel : IComparable, IEquatable<LogLevel>, IConvertible
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00007A38 File Offset: 0x00005C38
		public static IEnumerable<LogLevel> AllLevels
		{
			get
			{
				return LogLevel.allLevels;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00007A3F File Offset: 0x00005C3F
		public static IEnumerable<LogLevel> AllLoggingLevels
		{
			get
			{
				return LogLevel.allLoggingLevels;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00007A46 File Offset: 0x00005C46
		private LogLevel(string name, int ordinal)
		{
			this._name = name;
			this._ordinal = ordinal;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00007A5C File Offset: 0x00005C5C
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00007A64 File Offset: 0x00005C64
		internal static LogLevel MaxLevel
		{
			get
			{
				return LogLevel.Fatal;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00007A6B File Offset: 0x00005C6B
		internal static LogLevel MinLevel
		{
			get
			{
				return LogLevel.Trace;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00007A72 File Offset: 0x00005C72
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00007A7A File Offset: 0x00005C7A
		public static bool operator ==(LogLevel level1, LogLevel level2)
		{
			if (level1 == null)
			{
				return level2 == null;
			}
			return level2 != null && level1.Ordinal == level2.Ordinal;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00007A97 File Offset: 0x00005C97
		public static bool operator !=(LogLevel level1, LogLevel level2)
		{
			if (level1 == null)
			{
				return level2 != null;
			}
			return level2 == null || level1.Ordinal != level2.Ordinal;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00007AB7 File Offset: 0x00005CB7
		public static bool operator >(LogLevel level1, LogLevel level2)
		{
			if (level1 == null)
			{
				throw new ArgumentNullException("level1");
			}
			if (level2 == null)
			{
				throw new ArgumentNullException("level2");
			}
			return level1.Ordinal > level2.Ordinal;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00007AEF File Offset: 0x00005CEF
		public static bool operator >=(LogLevel level1, LogLevel level2)
		{
			if (level1 == null)
			{
				throw new ArgumentNullException("level1");
			}
			if (level2 == null)
			{
				throw new ArgumentNullException("level2");
			}
			return level1.Ordinal >= level2.Ordinal;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00007B2A File Offset: 0x00005D2A
		public static bool operator <(LogLevel level1, LogLevel level2)
		{
			if (level1 == null)
			{
				throw new ArgumentNullException("level1");
			}
			if (level2 == null)
			{
				throw new ArgumentNullException("level2");
			}
			return level1.Ordinal < level2.Ordinal;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00007B62 File Offset: 0x00005D62
		public static bool operator <=(LogLevel level1, LogLevel level2)
		{
			if (level1 == null)
			{
				throw new ArgumentNullException("level1");
			}
			if (level2 == null)
			{
				throw new ArgumentNullException("level2");
			}
			return level1.Ordinal <= level2.Ordinal;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00007BA0 File Offset: 0x00005DA0
		public static LogLevel FromOrdinal(int ordinal)
		{
			switch (ordinal)
			{
			case 0:
				return LogLevel.Trace;
			case 1:
				return LogLevel.Debug;
			case 2:
				return LogLevel.Info;
			case 3:
				return LogLevel.Warn;
			case 4:
				return LogLevel.Error;
			case 5:
				return LogLevel.Fatal;
			case 6:
				return LogLevel.Off;
			default:
				throw new ArgumentException("Invalid ordinal.");
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00007C08 File Offset: 0x00005E08
		public static LogLevel FromString(string levelName)
		{
			if (levelName == null)
			{
				throw new ArgumentNullException("levelName");
			}
			if (levelName.Equals("Trace", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Trace;
			}
			if (levelName.Equals("Debug", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Debug;
			}
			if (levelName.Equals("Info", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Info;
			}
			if (levelName.Equals("Warn", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Warn;
			}
			if (levelName.Equals("Error", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Error;
			}
			if (levelName.Equals("Fatal", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Fatal;
			}
			if (levelName.Equals("Off", StringComparison.OrdinalIgnoreCase))
			{
				return LogLevel.Off;
			}
			throw new ArgumentException("Unknown log level: " + levelName);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00007CBF File Offset: 0x00005EBF
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00007CC7 File Offset: 0x00005EC7
		public override int GetHashCode()
		{
			return this.Ordinal;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public override bool Equals(object obj)
		{
			LogLevel logLevel = obj as LogLevel;
			return logLevel != null && this.Ordinal == logLevel.Ordinal;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00007CF7 File Offset: 0x00005EF7
		public bool Equals(LogLevel other)
		{
			return other != null && this.Ordinal == other.Ordinal;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00007D14 File Offset: 0x00005F14
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			LogLevel logLevel = (LogLevel)obj;
			return this.Ordinal - logLevel.Ordinal;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00007D43 File Offset: 0x00005F43
		TypeCode IConvertible.GetTypeCode()
		{
			return TypeCode.Object;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00007D46 File Offset: 0x00005F46
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this._ordinal);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00007D53 File Offset: 0x00005F53
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00007D5A File Offset: 0x00005F5A
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this._ordinal);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00007D67 File Offset: 0x00005F67
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00007D6E File Offset: 0x00005F6E
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this._ordinal);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00007D7B File Offset: 0x00005F7B
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return (double)this._ordinal;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00007D84 File Offset: 0x00005F84
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this._ordinal);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00007D91 File Offset: 0x00005F91
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this._ordinal);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00007D9E File Offset: 0x00005F9E
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this._ordinal);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00007DAB File Offset: 0x00005FAB
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this._ordinal);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00007DB8 File Offset: 0x00005FB8
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this._ordinal);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00007DC5 File Offset: 0x00005FC5
		string IConvertible.ToString(IFormatProvider provider)
		{
			return this._name;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00007DCD File Offset: 0x00005FCD
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			if (conversionType == typeof(string))
			{
				return this.Name;
			}
			return Convert.ChangeType(this._ordinal, conversionType, provider);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00007DFA File Offset: 0x00005FFA
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this._ordinal);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00007E07 File Offset: 0x00006007
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this._ordinal);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00007E14 File Offset: 0x00006014
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this._ordinal);
		}

		// Token: 0x04000034 RID: 52
		public static readonly LogLevel Trace = new LogLevel("Trace", 0);

		// Token: 0x04000035 RID: 53
		public static readonly LogLevel Debug = new LogLevel("Debug", 1);

		// Token: 0x04000036 RID: 54
		public static readonly LogLevel Info = new LogLevel("Info", 2);

		// Token: 0x04000037 RID: 55
		public static readonly LogLevel Warn = new LogLevel("Warn", 3);

		// Token: 0x04000038 RID: 56
		public static readonly LogLevel Error = new LogLevel("Error", 4);

		// Token: 0x04000039 RID: 57
		public static readonly LogLevel Fatal = new LogLevel("Fatal", 5);

		// Token: 0x0400003A RID: 58
		public static readonly LogLevel Off = new LogLevel("Off", 6);

		// Token: 0x0400003B RID: 59
		private static readonly IList<LogLevel> allLevels = new List<LogLevel>
		{
			LogLevel.Trace,
			LogLevel.Debug,
			LogLevel.Info,
			LogLevel.Warn,
			LogLevel.Error,
			LogLevel.Fatal,
			LogLevel.Off
		}.AsReadOnly();

		// Token: 0x0400003C RID: 60
		private static readonly IList<LogLevel> allLoggingLevels = new List<LogLevel>
		{
			LogLevel.Trace,
			LogLevel.Debug,
			LogLevel.Info,
			LogLevel.Warn,
			LogLevel.Error,
			LogLevel.Fatal
		}.AsReadOnly();

		// Token: 0x0400003D RID: 61
		private readonly int _ordinal;

		// Token: 0x0400003E RID: 62
		private readonly string _name;
	}
}
