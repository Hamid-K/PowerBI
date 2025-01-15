using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000027 RID: 39
	[ImmutableObject(true)]
	internal sealed class EdmVariation
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000859D File Offset: 0x0000679D
		internal EdmVariation(string edmName, string referenceName, string navigationPropertyRef, string defaultHierarchyRef, string defaultPropertyRef, bool isDefault)
		{
			this._edmName = edmName;
			this._referenceName = referenceName;
			this._navigationPropertyRef = navigationPropertyRef;
			this._defaultHierarchyRef = defaultHierarchyRef;
			this._defaultPropertyRef = defaultPropertyRef;
			this._isDefault = isDefault;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000085D2 File Offset: 0x000067D2
		internal string EdmName
		{
			get
			{
				return this._edmName;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000085DA File Offset: 0x000067DA
		internal string ReferenceName
		{
			get
			{
				return this._referenceName;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000085E2 File Offset: 0x000067E2
		internal string NavigationPropertyRef
		{
			get
			{
				return this._navigationPropertyRef;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000085EA File Offset: 0x000067EA
		internal string DefaultHierarchyRef
		{
			get
			{
				return this._defaultHierarchyRef;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000085F2 File Offset: 0x000067F2
		internal string DefaultPropertyRef
		{
			get
			{
				return this._defaultPropertyRef;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000085FA File Offset: 0x000067FA
		internal bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		// Token: 0x04000167 RID: 359
		private readonly string _edmName;

		// Token: 0x04000168 RID: 360
		private readonly string _referenceName;

		// Token: 0x04000169 RID: 361
		private readonly string _navigationPropertyRef;

		// Token: 0x0400016A RID: 362
		private readonly string _defaultHierarchyRef;

		// Token: 0x0400016B RID: 363
		private readonly string _defaultPropertyRef;

		// Token: 0x0400016C RID: 364
		private readonly bool _isDefault;
	}
}
