using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000082 RID: 130
	internal sealed class TimeSpanTypeConverter : PrimitiveTypeConverter
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000E9F7 File Offset: 0x0000CBF7
		internal override object Parse(string text)
		{
			return EdmValueParser.ParseDuration(text);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000EA04 File Offset: 0x0000CC04
		internal override string ToString(object instance)
		{
			return EdmValueWriter.DurationAsXml((TimeSpan)instance);
		}
	}
}
