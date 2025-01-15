using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000059 RID: 89
	[EditorBrowsable(1)]
	public sealed class EmailUserEntityCollection : ComplexPropertyCollection<EmailUserEntity>
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000E3FA File Offset: 0x0000D3FA
		internal EmailUserEntityCollection()
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E402 File Offset: 0x0000D402
		internal EmailUserEntityCollection(IEnumerable<EmailUserEntity> collection)
		{
			if (collection != null)
			{
				collection.ForEach(new Action<EmailUserEntity>(base.InternalAdd));
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000E41F File Offset: 0x0000D41F
		internal override EmailUserEntity CreateComplexProperty(string xmlElementName)
		{
			return new EmailUserEntity();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000E426 File Offset: 0x0000D426
		internal override EmailUserEntity CreateDefaultComplexProperty()
		{
			return new EmailUserEntity();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000E42D File Offset: 0x0000D42D
		internal override string GetCollectionItemXmlElementName(EmailUserEntity complexProperty)
		{
			return "EmailUser";
		}
	}
}
