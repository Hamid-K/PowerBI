using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A9 RID: 1193
	public class EdmModel : MetadataItem
	{
		// Token: 0x06003A99 RID: 15001 RVA: 0x000C1794 File Offset: 0x000BF994
		private EdmModel(EntityContainer entityContainer, double version = 3.0)
		{
			this._container = entityContainer;
			this.SchemaVersion = version;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000C17EC File Offset: 0x000BF9EC
		internal EdmModel(DataSpace dataSpace, double schemaVersion = 3.0)
		{
			if (dataSpace != DataSpace.CSpace && dataSpace != DataSpace.SSpace)
			{
				throw new ArgumentException(Strings.MetadataItem_InvalidDataSpace(dataSpace, typeof(EdmModel).Name), "dataSpace");
			}
			this._container = new EntityContainer((dataSpace == DataSpace.CSpace) ? "CodeFirstContainer" : "CodeFirstDatabase", dataSpace);
			this._schemaVersion = schemaVersion;
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003A9B RID: 15003 RVA: 0x000C1886 File Offset: 0x000BFA86
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataItem;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x000C188A File Offset: 0x000BFA8A
		internal override string Identity
		{
			get
			{
				return "EdmModel" + this.Container.Identity;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x000C18A1 File Offset: 0x000BFAA1
		public DataSpace DataSpace
		{
			get
			{
				return this.Container.DataSpace;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003A9E RID: 15006 RVA: 0x000C18AE File Offset: 0x000BFAAE
		public IEnumerable<AssociationType> AssociationTypes
		{
			get
			{
				return this._associationTypes;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003A9F RID: 15007 RVA: 0x000C18B6 File Offset: 0x000BFAB6
		public IEnumerable<ComplexType> ComplexTypes
		{
			get
			{
				return this._complexTypes;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003AA0 RID: 15008 RVA: 0x000C18BE File Offset: 0x000BFABE
		public IEnumerable<EntityType> EntityTypes
		{
			get
			{
				return this._entityTypes;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x000C18C6 File Offset: 0x000BFAC6
		public IEnumerable<EnumType> EnumTypes
		{
			get
			{
				return this._enumTypes;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x000C18CE File Offset: 0x000BFACE
		public IEnumerable<EdmFunction> Functions
		{
			get
			{
				return this._functions;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x000C18D6 File Offset: 0x000BFAD6
		public EntityContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000C18DE File Offset: 0x000BFADE
		// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x000C18E6 File Offset: 0x000BFAE6
		internal double SchemaVersion
		{
			get
			{
				return this._schemaVersion;
			}
			set
			{
				this._schemaVersion = value;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x000C18EF File Offset: 0x000BFAEF
		// (set) Token: 0x06003AA7 RID: 15015 RVA: 0x000C18F7 File Offset: 0x000BFAF7
		internal DbProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
			private set
			{
				this._providerInfo = value;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000C1900 File Offset: 0x000BFB00
		// (set) Token: 0x06003AA9 RID: 15017 RVA: 0x000C1908 File Offset: 0x000BFB08
		internal DbProviderManifest ProviderManifest
		{
			get
			{
				return this._providerManifest;
			}
			private set
			{
				this._providerManifest = value;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000C1911 File Offset: 0x000BFB11
		internal virtual IEnumerable<string> NamespaceNames
		{
			get
			{
				return this.NamespaceItems.Select((EdmType t) => t.NamespaceName).Distinct<string>();
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x000C1942 File Offset: 0x000BFB42
		internal IEnumerable<EdmType> NamespaceItems
		{
			get
			{
				return this._associationTypes.Concat(this._complexTypes).Concat(this._entityTypes).Concat(this._enumTypes)
					.Concat(this._functions);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x000C1976 File Offset: 0x000BFB76
		public IEnumerable<GlobalItem> GlobalItems
		{
			get
			{
				return this.NamespaceItems.Concat(this.Containers);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x000C1989 File Offset: 0x000BFB89
		internal virtual IEnumerable<EntityContainer> Containers
		{
			get
			{
				yield return this.Container;
				yield break;
			}
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000C1999 File Offset: 0x000BFB99
		public void AddItem(AssociationType item)
		{
			Check.NotNull<AssociationType>(item, "item");
			this.ValidateSpace(item);
			this._associationTypes.Add(item);
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x000C19BA File Offset: 0x000BFBBA
		public void AddItem(ComplexType item)
		{
			Check.NotNull<ComplexType>(item, "item");
			this.ValidateSpace(item);
			this._complexTypes.Add(item);
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x000C19DB File Offset: 0x000BFBDB
		public void AddItem(EntityType item)
		{
			Check.NotNull<EntityType>(item, "item");
			this.ValidateSpace(item);
			this._entityTypes.Add(item);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x000C19FC File Offset: 0x000BFBFC
		public void AddItem(EnumType item)
		{
			Check.NotNull<EnumType>(item, "item");
			this.ValidateSpace(item);
			this._enumTypes.Add(item);
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x000C1A1D File Offset: 0x000BFC1D
		public void AddItem(EdmFunction item)
		{
			Check.NotNull<EdmFunction>(item, "item");
			this.ValidateSpace(item);
			this._functions.Add(item);
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x000C1A3E File Offset: 0x000BFC3E
		public void RemoveItem(AssociationType item)
		{
			Check.NotNull<AssociationType>(item, "item");
			this._associationTypes.Remove(item);
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x000C1A59 File Offset: 0x000BFC59
		public void RemoveItem(ComplexType item)
		{
			Check.NotNull<ComplexType>(item, "item");
			this._complexTypes.Remove(item);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x000C1A74 File Offset: 0x000BFC74
		public void RemoveItem(EntityType item)
		{
			Check.NotNull<EntityType>(item, "item");
			this._entityTypes.Remove(item);
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x000C1A8F File Offset: 0x000BFC8F
		public void RemoveItem(EnumType item)
		{
			Check.NotNull<EnumType>(item, "item");
			this._enumTypes.Remove(item);
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x000C1AAA File Offset: 0x000BFCAA
		public void RemoveItem(EdmFunction item)
		{
			Check.NotNull<EdmFunction>(item, "item");
			this._functions.Remove(item);
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x000C1AC8 File Offset: 0x000BFCC8
		internal virtual void Validate()
		{
			List<DataModelErrorEventArgs> validationErrors = new List<DataModelErrorEventArgs>();
			DataModelValidator dataModelValidator = new DataModelValidator();
			dataModelValidator.OnError += delegate(object _, DataModelErrorEventArgs e)
			{
				validationErrors.Add(e);
			};
			dataModelValidator.Validate(this, true);
			if (validationErrors.Count > 0)
			{
				throw new ModelValidationException(validationErrors);
			}
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000C1B1E File Offset: 0x000BFD1E
		private void ValidateSpace(EdmType item)
		{
			if (item.DataSpace != this.DataSpace)
			{
				throw new ArgumentException(Strings.EdmModel_AddItem_NonMatchingNamespace, "item");
			}
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000C1B3E File Offset: 0x000BFD3E
		internal static EdmModel CreateStoreModel(DbProviderInfo providerInfo, DbProviderManifest providerManifest, double schemaVersion = 3.0)
		{
			return new EdmModel(DataSpace.SSpace, schemaVersion)
			{
				ProviderInfo = providerInfo,
				ProviderManifest = providerManifest
			};
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000C1B58 File Offset: 0x000BFD58
		internal static EdmModel CreateStoreModel(EntityContainer entityContainer, DbProviderInfo providerInfo, DbProviderManifest providerManifest, double schemaVersion = 3.0)
		{
			EdmModel edmModel = new EdmModel(entityContainer, schemaVersion);
			if (providerInfo != null)
			{
				edmModel.ProviderInfo = providerInfo;
			}
			if (providerManifest != null)
			{
				edmModel.ProviderManifest = providerManifest;
			}
			return edmModel;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000C1B82 File Offset: 0x000BFD82
		internal static EdmModel CreateConceptualModel(double schemaVersion = 3.0)
		{
			return new EdmModel(DataSpace.CSpace, schemaVersion);
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000C1B8B File Offset: 0x000BFD8B
		internal static EdmModel CreateConceptualModel(EntityContainer entityContainer, double schemaVersion = 3.0)
		{
			return new EdmModel(entityContainer, schemaVersion);
		}

		// Token: 0x04001426 RID: 5158
		private readonly List<AssociationType> _associationTypes = new List<AssociationType>();

		// Token: 0x04001427 RID: 5159
		private readonly List<ComplexType> _complexTypes = new List<ComplexType>();

		// Token: 0x04001428 RID: 5160
		private readonly List<EntityType> _entityTypes = new List<EntityType>();

		// Token: 0x04001429 RID: 5161
		private readonly List<EnumType> _enumTypes = new List<EnumType>();

		// Token: 0x0400142A RID: 5162
		private readonly List<EdmFunction> _functions = new List<EdmFunction>();

		// Token: 0x0400142B RID: 5163
		private readonly EntityContainer _container;

		// Token: 0x0400142C RID: 5164
		private double _schemaVersion;

		// Token: 0x0400142D RID: 5165
		private DbProviderInfo _providerInfo;

		// Token: 0x0400142E RID: 5166
		private DbProviderManifest _providerManifest;
	}
}
