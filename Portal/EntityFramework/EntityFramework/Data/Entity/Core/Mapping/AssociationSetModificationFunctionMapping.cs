using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200051E RID: 1310
	public sealed class AssociationSetModificationFunctionMapping : MappingItem
	{
		// Token: 0x06004083 RID: 16515 RVA: 0x000D9AB4 File Offset: 0x000D7CB4
		public AssociationSetModificationFunctionMapping(AssociationSet associationSet, ModificationFunctionMapping deleteFunctionMapping, ModificationFunctionMapping insertFunctionMapping)
		{
			Check.NotNull<AssociationSet>(associationSet, "associationSet");
			this._associationSet = associationSet;
			this._deleteFunctionMapping = deleteFunctionMapping;
			this._insertFunctionMapping = insertFunctionMapping;
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004084 RID: 16516 RVA: 0x000D9ADD File Offset: 0x000D7CDD
		public AssociationSet AssociationSet
		{
			get
			{
				return this._associationSet;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004085 RID: 16517 RVA: 0x000D9AE5 File Offset: 0x000D7CE5
		public ModificationFunctionMapping DeleteFunctionMapping
		{
			get
			{
				return this._deleteFunctionMapping;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000D9AED File Offset: 0x000D7CED
		public ModificationFunctionMapping InsertFunctionMapping
		{
			get
			{
				return this._insertFunctionMapping;
			}
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x000D9AF8 File Offset: 0x000D7CF8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "AS{{{0}}}:{3}DFunc={{{1}}},{3}IFunc={{{2}}}", new object[]
			{
				this.AssociationSet,
				this.DeleteFunctionMapping,
				this.InsertFunctionMapping,
				Environment.NewLine + "  "
			});
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x000D9B47 File Offset: 0x000D7D47
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._deleteFunctionMapping);
			MappingItem.SetReadOnly(this._insertFunctionMapping);
			base.SetReadOnly();
		}

		// Token: 0x04001671 RID: 5745
		private readonly AssociationSet _associationSet;

		// Token: 0x04001672 RID: 5746
		private readonly ModificationFunctionMapping _deleteFunctionMapping;

		// Token: 0x04001673 RID: 5747
		private readonly ModificationFunctionMapping _insertFunctionMapping;
	}
}
