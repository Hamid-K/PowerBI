using System;
using System.Data.Entity.Infrastructure;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000076 RID: 118
	internal static class DbModelExtensions
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public static XDocument GetModel(this DbModel model)
		{
			return DbContextExtensions.GetModel(delegate(XmlWriter w)
			{
				EdmxWriter.WriteEdmx(model, w);
			});
		}
	}
}
