using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200015A RID: 346
	[DataContract(Name = "ConceptualHierarchy", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualHierarchy
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x000121A7 File Offset: 0x000103A7
		internal ClientConceptualHierarchy()
		{
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000121B0 File Offset: 0x000103B0
		internal ClientConceptualHierarchy(string name, string displayName, string description, bool isHidden, IList<ClientConceptualHierarchyLevel> levels, bool canDelete, string stableName)
		{
			this._name = name;
			if (name != displayName)
			{
				this._displayName = displayName;
			}
			this._description = description;
			this._isHidden = isHidden;
			this._levels = levels;
			this._canDelete = ClientConceptualSchemaFactory.ConvertTrueToNull(canDelete);
			this._stableName = stableName;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00012206 File Offset: 0x00010406
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0001220E File Offset: 0x0001040E
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00012216 File Offset: 0x00010416
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0001221E File Offset: 0x0001041E
		public IList<ClientConceptualHierarchyLevel> Levels
		{
			get
			{
				return this._levels;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00012226 File Offset: 0x00010426
		public bool? CanDelete
		{
			get
			{
				return this._canDelete;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0001222E File Offset: 0x0001042E
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00012236 File Offset: 0x00010436
		public string StableName
		{
			get
			{
				return this._stableName;
			}
		}

		// Token: 0x04000452 RID: 1106
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private string _name;

		// Token: 0x04000453 RID: 1107
		[DataMember(Name = "DisplayName", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private string _displayName;

		// Token: 0x04000454 RID: 1108
		[DataMember(Name = "Hidden", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		private bool _isHidden;

		// Token: 0x04000455 RID: 1109
		[DataMember(Name = "Levels", IsRequired = false, EmitDefaultValue = false, Order = 3)]
		private IList<ClientConceptualHierarchyLevel> _levels;

		// Token: 0x04000456 RID: 1110
		[DataMember(Name = "CanDelete", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		private bool? _canDelete;

		// Token: 0x04000457 RID: 1111
		[DataMember(Name = "Description", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		private string _description;

		// Token: 0x04000458 RID: 1112
		[DataMember(Name = "StableName", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		private string _stableName;
	}
}
