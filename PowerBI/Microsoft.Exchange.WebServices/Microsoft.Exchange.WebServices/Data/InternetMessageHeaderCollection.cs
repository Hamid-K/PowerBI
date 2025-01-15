using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200006C RID: 108
	[EditorBrowsable(1)]
	public sealed class InternetMessageHeaderCollection : ComplexPropertyCollection<InternetMessageHeader>
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00011F48 File Offset: 0x00010F48
		internal InternetMessageHeaderCollection()
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00011F50 File Offset: 0x00010F50
		internal override InternetMessageHeader CreateComplexProperty(string xmlElementName)
		{
			return new InternetMessageHeader();
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00011F57 File Offset: 0x00010F57
		internal override InternetMessageHeader CreateDefaultComplexProperty()
		{
			return new InternetMessageHeader();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00011F5E File Offset: 0x00010F5E
		internal override string GetCollectionItemXmlElementName(InternetMessageHeader complexProperty)
		{
			return "InternetMessageHeader";
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00011F68 File Offset: 0x00010F68
		public InternetMessageHeader Find(string name)
		{
			foreach (InternetMessageHeader internetMessageHeader in this)
			{
				if (string.Compare(name, internetMessageHeader.Name, 5) == 0)
				{
					return internetMessageHeader;
				}
			}
			return null;
		}
	}
}
