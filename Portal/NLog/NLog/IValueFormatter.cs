using System;
using System.Text;
using NLog.MessageTemplates;

namespace NLog
{
	// Token: 0x0200000A RID: 10
	public interface IValueFormatter
	{
		// Token: 0x06000184 RID: 388
		bool FormatValue(object value, string format, CaptureType captureType, IFormatProvider formatProvider, StringBuilder builder);
	}
}
