using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200015B RID: 347
	[DataContract(Name = "ConceptualHierarchyLevel", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualHierarchyLevel
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x0001223E File Offset: 0x0001043E
		internal ClientConceptualHierarchyLevel()
		{
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00012246 File Offset: 0x00010446
		internal ClientConceptualHierarchyLevel(string name, string displayName, string column, bool canDelete, string stableName)
		{
			this._name = name;
			if (name != displayName)
			{
				this._displayName = displayName;
			}
			this._column = column;
			this._canDelete = ClientConceptualSchemaFactory.ConvertTrueToNull(canDelete);
			this._stableName = stableName;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00012281 File Offset: 0x00010481
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00012289 File Offset: 0x00010489
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00012291 File Offset: 0x00010491
		public string Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00012299 File Offset: 0x00010499
		public bool? CanDelete
		{
			get
			{
				return this._canDelete;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000122A1 File Offset: 0x000104A1
		public string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x04000459 RID: 1113
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private string _name;

		// Token: 0x0400045A RID: 1114
		[DataMember(Name = "DisplayName", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private string _displayName;

		// Token: 0x0400045B RID: 1115
		[DataMember(Name = "Column", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private string _column;

		// Token: 0x0400045C RID: 1116
		[DataMember(Name = "CanDelete", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private bool? _canDelete;

		// Token: 0x0400045D RID: 1117
		[DataMember(Name = "StableName", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private string _stableName;
	}
}
