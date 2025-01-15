using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000B4 RID: 180
	internal class CacheGenerationNumber
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00014D84 File Offset: 0x00012F84
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00014D8C File Offset: 0x00012F8C
		public long Number { get; private set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00014D95 File Offset: 0x00012F95
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00014D9D File Offset: 0x00012F9D
		public string Owner { get; private set; }

		// Token: 0x06000449 RID: 1097 RVA: 0x00014DA6 File Offset: 0x00012FA6
		public CacheGenerationNumber(GenerationNumber number)
		{
			this.Number = (long)CacheGenerationNumber.GetPrivateFieldValue(number, "m_generation");
			this.Owner = (string)CacheGenerationNumber.GetPrivateFieldValue(number, "m_owner");
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00014DDC File Offset: 0x00012FDC
		private static object GetPrivateFieldValue(object obj, string propName)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Type type = obj.GetType();
			FieldInfo fieldInfo = null;
			while (fieldInfo == null && type != null)
			{
				fieldInfo = type.GetField(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				type = type.BaseType;
			}
			if (fieldInfo == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Field {0} was not found in Type {1}", new object[]
				{
					propName,
					obj.GetType().FullName
				}), "propName");
			}
			return fieldInfo.GetValue(obj);
		}
	}
}
