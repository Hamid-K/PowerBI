using System;
using System.ComponentModel;

namespace NLog
{
	// Token: 0x02000006 RID: 6
	[CLSCompliant(false)]
	public interface ILogger : ILoggerBase, ISuppress
	{
		// Token: 0x06000020 RID: 32
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(object value);

		// Token: 0x06000021 RID: 33
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Trace(IFormatProvider formatProvider, object value);

		// Token: 0x06000022 RID: 34
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, object arg1, object arg2);

		// Token: 0x06000023 RID: 35
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000024 RID: 36
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x06000025 RID: 37
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, bool argument);

		// Token: 0x06000026 RID: 38
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x06000027 RID: 39
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, char argument);

		// Token: 0x06000028 RID: 40
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x06000029 RID: 41
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, byte argument);

		// Token: 0x0600002A RID: 42
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x0600002B RID: 43
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, string argument);

		// Token: 0x0600002C RID: 44
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x0600002D RID: 45
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, int argument);

		// Token: 0x0600002E RID: 46
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x0600002F RID: 47
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, long argument);

		// Token: 0x06000030 RID: 48
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x06000031 RID: 49
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, float argument);

		// Token: 0x06000032 RID: 50
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x06000033 RID: 51
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, double argument);

		// Token: 0x06000034 RID: 52
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x06000035 RID: 53
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, decimal argument);

		// Token: 0x06000036 RID: 54
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x06000037 RID: 55
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, object argument);

		// Token: 0x06000038 RID: 56
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x06000039 RID: 57
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, sbyte argument);

		// Token: 0x0600003A RID: 58
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x0600003B RID: 59
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, uint argument);

		// Token: 0x0600003C RID: 60
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x0600003D RID: 61
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Trace(string message, ulong argument);

		// Token: 0x0600003E RID: 62
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(object value);

		// Token: 0x0600003F RID: 63
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Debug(IFormatProvider formatProvider, object value);

		// Token: 0x06000040 RID: 64
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, object arg1, object arg2);

		// Token: 0x06000041 RID: 65
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000042 RID: 66
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x06000043 RID: 67
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, bool argument);

		// Token: 0x06000044 RID: 68
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x06000045 RID: 69
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, char argument);

		// Token: 0x06000046 RID: 70
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x06000047 RID: 71
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, byte argument);

		// Token: 0x06000048 RID: 72
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x06000049 RID: 73
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, string argument);

		// Token: 0x0600004A RID: 74
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x0600004B RID: 75
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, int argument);

		// Token: 0x0600004C RID: 76
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x0600004D RID: 77
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, long argument);

		// Token: 0x0600004E RID: 78
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x0600004F RID: 79
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, float argument);

		// Token: 0x06000050 RID: 80
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x06000051 RID: 81
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, double argument);

		// Token: 0x06000052 RID: 82
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x06000053 RID: 83
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, decimal argument);

		// Token: 0x06000054 RID: 84
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x06000055 RID: 85
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, object argument);

		// Token: 0x06000056 RID: 86
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x06000057 RID: 87
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, sbyte argument);

		// Token: 0x06000058 RID: 88
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x06000059 RID: 89
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, uint argument);

		// Token: 0x0600005A RID: 90
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x0600005B RID: 91
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Debug(string message, ulong argument);

		// Token: 0x0600005C RID: 92
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(object value);

		// Token: 0x0600005D RID: 93
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Info(IFormatProvider formatProvider, object value);

		// Token: 0x0600005E RID: 94
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, object arg1, object arg2);

		// Token: 0x0600005F RID: 95
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, object arg1, object arg2, object arg3);

		// Token: 0x06000060 RID: 96
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x06000061 RID: 97
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, bool argument);

		// Token: 0x06000062 RID: 98
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x06000063 RID: 99
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, char argument);

		// Token: 0x06000064 RID: 100
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x06000065 RID: 101
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, byte argument);

		// Token: 0x06000066 RID: 102
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x06000067 RID: 103
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, string argument);

		// Token: 0x06000068 RID: 104
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x06000069 RID: 105
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, int argument);

		// Token: 0x0600006A RID: 106
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x0600006B RID: 107
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, long argument);

		// Token: 0x0600006C RID: 108
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x0600006D RID: 109
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, float argument);

		// Token: 0x0600006E RID: 110
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x0600006F RID: 111
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, double argument);

		// Token: 0x06000070 RID: 112
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x06000071 RID: 113
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, decimal argument);

		// Token: 0x06000072 RID: 114
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x06000073 RID: 115
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, object argument);

		// Token: 0x06000074 RID: 116
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x06000075 RID: 117
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, sbyte argument);

		// Token: 0x06000076 RID: 118
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x06000077 RID: 119
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, uint argument);

		// Token: 0x06000078 RID: 120
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x06000079 RID: 121
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Info(string message, ulong argument);

		// Token: 0x0600007A RID: 122
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(object value);

		// Token: 0x0600007B RID: 123
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Warn(IFormatProvider formatProvider, object value);

		// Token: 0x0600007C RID: 124
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, object arg1, object arg2);

		// Token: 0x0600007D RID: 125
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, object arg1, object arg2, object arg3);

		// Token: 0x0600007E RID: 126
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x0600007F RID: 127
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, bool argument);

		// Token: 0x06000080 RID: 128
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x06000081 RID: 129
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, char argument);

		// Token: 0x06000082 RID: 130
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x06000083 RID: 131
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, byte argument);

		// Token: 0x06000084 RID: 132
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x06000085 RID: 133
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, string argument);

		// Token: 0x06000086 RID: 134
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x06000087 RID: 135
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, int argument);

		// Token: 0x06000088 RID: 136
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x06000089 RID: 137
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, long argument);

		// Token: 0x0600008A RID: 138
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x0600008B RID: 139
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, float argument);

		// Token: 0x0600008C RID: 140
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x0600008D RID: 141
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, double argument);

		// Token: 0x0600008E RID: 142
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x0600008F RID: 143
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, decimal argument);

		// Token: 0x06000090 RID: 144
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x06000091 RID: 145
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, object argument);

		// Token: 0x06000092 RID: 146
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x06000093 RID: 147
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, sbyte argument);

		// Token: 0x06000094 RID: 148
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x06000095 RID: 149
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, uint argument);

		// Token: 0x06000096 RID: 150
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x06000097 RID: 151
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Warn(string message, ulong argument);

		// Token: 0x06000098 RID: 152
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(object value);

		// Token: 0x06000099 RID: 153
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(IFormatProvider formatProvider, object value);

		// Token: 0x0600009A RID: 154
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, object arg1, object arg2);

		// Token: 0x0600009B RID: 155
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, object arg1, object arg2, object arg3);

		// Token: 0x0600009C RID: 156
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x0600009D RID: 157
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, bool argument);

		// Token: 0x0600009E RID: 158
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x0600009F RID: 159
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, char argument);

		// Token: 0x060000A0 RID: 160
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x060000A1 RID: 161
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, byte argument);

		// Token: 0x060000A2 RID: 162
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x060000A3 RID: 163
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, string argument);

		// Token: 0x060000A4 RID: 164
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x060000A5 RID: 165
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, int argument);

		// Token: 0x060000A6 RID: 166
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x060000A7 RID: 167
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, long argument);

		// Token: 0x060000A8 RID: 168
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x060000A9 RID: 169
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Error(string message, float argument);

		// Token: 0x060000AA RID: 170
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x060000AB RID: 171
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, double argument);

		// Token: 0x060000AC RID: 172
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x060000AD RID: 173
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, decimal argument);

		// Token: 0x060000AE RID: 174
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x060000AF RID: 175
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, object argument);

		// Token: 0x060000B0 RID: 176
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x060000B1 RID: 177
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, sbyte argument);

		// Token: 0x060000B2 RID: 178
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x060000B3 RID: 179
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, uint argument);

		// Token: 0x060000B4 RID: 180
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x060000B5 RID: 181
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Error(string message, ulong argument);

		// Token: 0x060000B6 RID: 182
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(object value);

		// Token: 0x060000B7 RID: 183
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(IFormatProvider formatProvider, object value);

		// Token: 0x060000B8 RID: 184
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, object arg1, object arg2);

		// Token: 0x060000B9 RID: 185
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, object arg1, object arg2, object arg3);

		// Token: 0x060000BA RID: 186
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, bool argument);

		// Token: 0x060000BB RID: 187
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, bool argument);

		// Token: 0x060000BC RID: 188
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, char argument);

		// Token: 0x060000BD RID: 189
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, char argument);

		// Token: 0x060000BE RID: 190
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, byte argument);

		// Token: 0x060000BF RID: 191
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, byte argument);

		// Token: 0x060000C0 RID: 192
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, string argument);

		// Token: 0x060000C1 RID: 193
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, string argument);

		// Token: 0x060000C2 RID: 194
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, int argument);

		// Token: 0x060000C3 RID: 195
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, int argument);

		// Token: 0x060000C4 RID: 196
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, long argument);

		// Token: 0x060000C5 RID: 197
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, long argument);

		// Token: 0x060000C6 RID: 198
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, float argument);

		// Token: 0x060000C7 RID: 199
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, float argument);

		// Token: 0x060000C8 RID: 200
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, double argument);

		// Token: 0x060000C9 RID: 201
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, double argument);

		// Token: 0x060000CA RID: 202
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, decimal argument);

		// Token: 0x060000CB RID: 203
		[EditorBrowsable(EditorBrowsableState.Never)]
		void Fatal(string message, decimal argument);

		// Token: 0x060000CC RID: 204
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, object argument);

		// Token: 0x060000CD RID: 205
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, object argument);

		// Token: 0x060000CE RID: 206
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, sbyte argument);

		// Token: 0x060000CF RID: 207
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, sbyte argument);

		// Token: 0x060000D0 RID: 208
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, uint argument);

		// Token: 0x060000D1 RID: 209
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, uint argument);

		// Token: 0x060000D2 RID: 210
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, string message, ulong argument);

		// Token: 0x060000D3 RID: 211
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MessageTemplateFormatMethod("message")]
		void Fatal(string message, ulong argument);

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000D4 RID: 212
		bool IsTraceEnabled { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000D5 RID: 213
		bool IsDebugEnabled { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000D6 RID: 214
		bool IsInfoEnabled { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000D7 RID: 215
		bool IsWarnEnabled { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000D8 RID: 216
		bool IsErrorEnabled { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000D9 RID: 217
		bool IsFatalEnabled { get; }

		// Token: 0x060000DA RID: 218
		void Trace<T>(T value);

		// Token: 0x060000DB RID: 219
		void Trace<T>(IFormatProvider formatProvider, T value);

		// Token: 0x060000DC RID: 220
		void Trace(LogMessageGenerator messageFunc);

		// Token: 0x060000DD RID: 221
		[Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void TraceException([Localizable(false)] string message, Exception exception);

		// Token: 0x060000DE RID: 222
		void Trace(Exception exception, [Localizable(false)] string message);

		// Token: 0x060000DF RID: 223
		[MessageTemplateFormatMethod("message")]
		void Trace(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x060000E0 RID: 224
		[MessageTemplateFormatMethod("message")]
		void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060000E1 RID: 225
		[MessageTemplateFormatMethod("message")]
		void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060000E2 RID: 226
		void Trace([Localizable(false)] string message);

		// Token: 0x060000E3 RID: 227
		[MessageTemplateFormatMethod("message")]
		void Trace([Localizable(false)] string message, params object[] args);

		// Token: 0x060000E4 RID: 228
		[Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void Trace([Localizable(false)] string message, Exception exception);

		// Token: 0x060000E5 RID: 229
		[MessageTemplateFormatMethod("message")]
		void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x060000E6 RID: 230
		[MessageTemplateFormatMethod("message")]
		void Trace<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x060000E7 RID: 231
		[MessageTemplateFormatMethod("message")]
		void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060000E8 RID: 232
		[MessageTemplateFormatMethod("message")]
		void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060000E9 RID: 233
		[MessageTemplateFormatMethod("message")]
		void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060000EA RID: 234
		[MessageTemplateFormatMethod("message")]
		void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060000EB RID: 235
		void Debug<T>(T value);

		// Token: 0x060000EC RID: 236
		void Debug<T>(IFormatProvider formatProvider, T value);

		// Token: 0x060000ED RID: 237
		void Debug(LogMessageGenerator messageFunc);

		// Token: 0x060000EE RID: 238
		[Obsolete("Use Debug(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void DebugException([Localizable(false)] string message, Exception exception);

		// Token: 0x060000EF RID: 239
		void Debug(Exception exception, [Localizable(false)] string message);

		// Token: 0x060000F0 RID: 240
		[MessageTemplateFormatMethod("message")]
		void Debug(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x060000F1 RID: 241
		[MessageTemplateFormatMethod("message")]
		void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060000F2 RID: 242
		[MessageTemplateFormatMethod("message")]
		void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x060000F3 RID: 243
		void Debug([Localizable(false)] string message);

		// Token: 0x060000F4 RID: 244
		[MessageTemplateFormatMethod("message")]
		void Debug([Localizable(false)] string message, params object[] args);

		// Token: 0x060000F5 RID: 245
		[Obsolete("Use Debug(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void Debug([Localizable(false)] string message, Exception exception);

		// Token: 0x060000F6 RID: 246
		[MessageTemplateFormatMethod("message")]
		void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x060000F7 RID: 247
		[MessageTemplateFormatMethod("message")]
		void Debug<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x060000F8 RID: 248
		[MessageTemplateFormatMethod("message")]
		void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060000F9 RID: 249
		[MessageTemplateFormatMethod("message")]
		void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x060000FA RID: 250
		[MessageTemplateFormatMethod("message")]
		void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060000FB RID: 251
		[MessageTemplateFormatMethod("message")]
		void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x060000FC RID: 252
		void Info<T>(T value);

		// Token: 0x060000FD RID: 253
		void Info<T>(IFormatProvider formatProvider, T value);

		// Token: 0x060000FE RID: 254
		void Info(LogMessageGenerator messageFunc);

		// Token: 0x060000FF RID: 255
		[Obsolete("Use Info(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void InfoException([Localizable(false)] string message, Exception exception);

		// Token: 0x06000100 RID: 256
		void Info(Exception exception, [Localizable(false)] string message);

		// Token: 0x06000101 RID: 257
		[MessageTemplateFormatMethod("message")]
		void Info(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000102 RID: 258
		[MessageTemplateFormatMethod("message")]
		void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000103 RID: 259
		[MessageTemplateFormatMethod("message")]
		void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000104 RID: 260
		void Info([Localizable(false)] string message);

		// Token: 0x06000105 RID: 261
		[MessageTemplateFormatMethod("message")]
		void Info([Localizable(false)] string message, params object[] args);

		// Token: 0x06000106 RID: 262
		[Obsolete("Use Info(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void Info([Localizable(false)] string message, Exception exception);

		// Token: 0x06000107 RID: 263
		[MessageTemplateFormatMethod("message")]
		void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x06000108 RID: 264
		[MessageTemplateFormatMethod("message")]
		void Info<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x06000109 RID: 265
		[MessageTemplateFormatMethod("message")]
		void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600010A RID: 266
		[MessageTemplateFormatMethod("message")]
		void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600010B RID: 267
		[MessageTemplateFormatMethod("message")]
		void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600010C RID: 268
		[MessageTemplateFormatMethod("message")]
		void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600010D RID: 269
		void Warn<T>(T value);

		// Token: 0x0600010E RID: 270
		void Warn<T>(IFormatProvider formatProvider, T value);

		// Token: 0x0600010F RID: 271
		void Warn(LogMessageGenerator messageFunc);

		// Token: 0x06000110 RID: 272
		[Obsolete("Use Warn(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void WarnException([Localizable(false)] string message, Exception exception);

		// Token: 0x06000111 RID: 273
		void Warn(Exception exception, [Localizable(false)] string message);

		// Token: 0x06000112 RID: 274
		[MessageTemplateFormatMethod("message")]
		void Warn(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000113 RID: 275
		[MessageTemplateFormatMethod("message")]
		void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000114 RID: 276
		[MessageTemplateFormatMethod("message")]
		void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000115 RID: 277
		void Warn([Localizable(false)] string message);

		// Token: 0x06000116 RID: 278
		[MessageTemplateFormatMethod("message")]
		void Warn([Localizable(false)] string message, params object[] args);

		// Token: 0x06000117 RID: 279
		[Obsolete("Use Warn(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void Warn([Localizable(false)] string message, Exception exception);

		// Token: 0x06000118 RID: 280
		[MessageTemplateFormatMethod("message")]
		void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x06000119 RID: 281
		[MessageTemplateFormatMethod("message")]
		void Warn<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x0600011A RID: 282
		[MessageTemplateFormatMethod("message")]
		void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600011B RID: 283
		[MessageTemplateFormatMethod("message")]
		void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600011C RID: 284
		[MessageTemplateFormatMethod("message")]
		void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600011D RID: 285
		[MessageTemplateFormatMethod("message")]
		void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600011E RID: 286
		void Error<T>(T value);

		// Token: 0x0600011F RID: 287
		void Error<T>(IFormatProvider formatProvider, T value);

		// Token: 0x06000120 RID: 288
		void Error(LogMessageGenerator messageFunc);

		// Token: 0x06000121 RID: 289
		[Obsolete("Use Error(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void ErrorException([Localizable(false)] string message, Exception exception);

		// Token: 0x06000122 RID: 290
		void Error(Exception exception, [Localizable(false)] string message);

		// Token: 0x06000123 RID: 291
		[MessageTemplateFormatMethod("message")]
		void Error(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000124 RID: 292
		[MessageTemplateFormatMethod("message")]
		void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000125 RID: 293
		[MessageTemplateFormatMethod("message")]
		void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000126 RID: 294
		void Error([Localizable(false)] string message);

		// Token: 0x06000127 RID: 295
		[MessageTemplateFormatMethod("message")]
		void Error([Localizable(false)] string message, params object[] args);

		// Token: 0x06000128 RID: 296
		[Obsolete("Use Error(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void Error([Localizable(false)] string message, Exception exception);

		// Token: 0x06000129 RID: 297
		[MessageTemplateFormatMethod("message")]
		void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x0600012A RID: 298
		[MessageTemplateFormatMethod("message")]
		void Error<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x0600012B RID: 299
		[MessageTemplateFormatMethod("message")]
		void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600012C RID: 300
		[MessageTemplateFormatMethod("message")]
		void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600012D RID: 301
		[MessageTemplateFormatMethod("message")]
		void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600012E RID: 302
		[MessageTemplateFormatMethod("message")]
		void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600012F RID: 303
		void Fatal<T>(T value);

		// Token: 0x06000130 RID: 304
		void Fatal<T>(IFormatProvider formatProvider, T value);

		// Token: 0x06000131 RID: 305
		void Fatal(LogMessageGenerator messageFunc);

		// Token: 0x06000132 RID: 306
		[Obsolete("Use Fatal(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void FatalException([Localizable(false)] string message, Exception exception);

		// Token: 0x06000133 RID: 307
		void Fatal(Exception exception, [Localizable(false)] string message);

		// Token: 0x06000134 RID: 308
		[MessageTemplateFormatMethod("message")]
		void Fatal(Exception exception, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000135 RID: 309
		[MessageTemplateFormatMethod("message")]
		void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000136 RID: 310
		[MessageTemplateFormatMethod("message")]
		void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

		// Token: 0x06000137 RID: 311
		void Fatal([Localizable(false)] string message);

		// Token: 0x06000138 RID: 312
		[MessageTemplateFormatMethod("message")]
		void Fatal([Localizable(false)] string message, params object[] args);

		// Token: 0x06000139 RID: 313
		[Obsolete("Use Fatal(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
		void Fatal([Localizable(false)] string message, Exception exception);

		// Token: 0x0600013A RID: 314
		[MessageTemplateFormatMethod("message")]
		void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

		// Token: 0x0600013B RID: 315
		[MessageTemplateFormatMethod("message")]
		void Fatal<TArgument>([Localizable(false)] string message, TArgument argument);

		// Token: 0x0600013C RID: 316
		[MessageTemplateFormatMethod("message")]
		void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600013D RID: 317
		[MessageTemplateFormatMethod("message")]
		void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2);

		// Token: 0x0600013E RID: 318
		[MessageTemplateFormatMethod("message")]
		void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

		// Token: 0x0600013F RID: 319
		[MessageTemplateFormatMethod("message")]
		void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);
	}
}
