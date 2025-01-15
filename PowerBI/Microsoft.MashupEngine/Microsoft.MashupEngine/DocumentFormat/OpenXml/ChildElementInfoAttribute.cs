using System;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020030D4 RID: 12500
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	internal sealed class ChildElementInfoAttribute : Attribute
	{
		// Token: 0x0601B274 RID: 111220 RVA: 0x0036E6CA File Offset: 0x0036C8CA
		public ChildElementInfoAttribute(Type elementType)
		{
			this._type = elementType;
			this.format = FileFormatVersions.Office2007 | FileFormatVersions.Office2010;
		}

		// Token: 0x0601B275 RID: 111221 RVA: 0x0036E6E0 File Offset: 0x0036C8E0
		public ChildElementInfoAttribute(Type elementType, FileFormatVersions availableInOfficeVersion)
		{
			this._type = elementType;
			this.format = availableInOfficeVersion;
		}

		// Token: 0x17009869 RID: 39017
		// (get) Token: 0x0601B276 RID: 111222 RVA: 0x0036E6F6 File Offset: 0x0036C8F6
		public Type ElementType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700986A RID: 39018
		// (get) Token: 0x0601B277 RID: 111223 RVA: 0x0036E6FE File Offset: 0x0036C8FE
		public FileFormatVersions AvailableInVersion
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x0400B3EB RID: 46059
		private Type _type;

		// Token: 0x0400B3EC RID: 46060
		private FileFormatVersions format;
	}
}
