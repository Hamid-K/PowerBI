using System;

namespace NLog.Config
{
	// Token: 0x0200018B RID: 395
	public interface IPropertyTypeConverter
	{
		// Token: 0x060011E2 RID: 4578
		object Convert(object propertyValue, Type propertyType, string format, IFormatProvider formatProvider);
	}
}
