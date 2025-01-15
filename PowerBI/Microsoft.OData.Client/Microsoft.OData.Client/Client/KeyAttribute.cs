using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Microsoft.OData.Client
{
	// Token: 0x020000BF RID: 191
	[SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Justification = "Accessors are available for processed input.")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class KeyAttribute : Attribute
	{
		// Token: 0x06000652 RID: 1618 RVA: 0x0001BDBA File Offset: 0x00019FBA
		public KeyAttribute(string keyName)
		{
			Util.CheckArgumentNull<string>(keyName, "keyName");
			Util.CheckArgumentNullAndEmpty(keyName, "KeyName");
			this.keyNames = new ReadOnlyCollection<string>(new string[] { keyName });
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		public KeyAttribute(params string[] keyNames)
		{
			Util.CheckArgumentNull<string[]>(keyNames, "keyNames");
			if (keyNames.Length != 0)
			{
				if (!keyNames.Any((string f) => f == null || f.Length == 0))
				{
					this.keyNames = new ReadOnlyCollection<string>(keyNames);
					return;
				}
			}
			throw Error.Argument(Strings.DSKAttribute_MustSpecifyAtleastOnePropertyName, "keyNames");
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001BE56 File Offset: 0x0001A056
		public ReadOnlyCollection<string> KeyNames
		{
			get
			{
				return this.keyNames;
			}
		}

		// Token: 0x040002D0 RID: 720
		private readonly ReadOnlyCollection<string> keyNames;
	}
}
