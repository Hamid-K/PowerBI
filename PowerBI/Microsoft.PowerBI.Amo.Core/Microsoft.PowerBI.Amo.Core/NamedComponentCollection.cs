using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000A6 RID: 166
	[Guid("4CF930A2-FED5-48c0-AC50-DD4FBDA3E6A8")]
	public abstract class NamedComponentCollection : ModelComponentCollection, INamedComponentCollection, IModelComponentCollection, ICollection, IEnumerable
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x0002763E File Offset: 0x0002583E
		protected NamedComponentCollection(IModelComponent parent)
			: base(parent)
		{
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00027647 File Offset: 0x00025847
		internal string DefaultNamePrefix
		{
			get
			{
				return this.ItemsType.Name;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00027654 File Offset: 0x00025854
		internal string DefaultIDPrefix
		{
			get
			{
				return this.ItemsType.Name;
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00027664 File Offset: 0x00025864
		internal INamedComponent BaseGetByName(string name, bool throwIfNotFound)
		{
			int num = this.IndexOfName(name);
			if (num != -1)
			{
				return (INamedComponent)base[num];
			}
			if (throwIfNotFound)
			{
				throw Utils.CreateItemNotFoundException(name, "Name", ClientHostingManager.MarkAsRestrictedInformation(this.ItemsType.Name, InfoRestrictionType.CCON));
			}
			return null;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000276AC File Offset: 0x000258AC
		public int IndexOfName(string name)
		{
			if (name == null)
			{
				return -1;
			}
			int i = 0;
			int count = base.Count;
			while (i < count)
			{
				string name2 = ((INamedComponent)base[i]).Name;
				if (string.Compare(name, name2, true, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
				if (string.Compare(name, name2, true, CultureInfo.CurrentCulture) == 0)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00027706 File Offset: 0x00025906
		public new int IndexOf(string id)
		{
			return base.IndexOf(id);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00027710 File Offset: 0x00025910
		public override bool CanAdd(ModelComponent item, out string error)
		{
			NamedComponent namedComponent = item as NamedComponent;
			ModelComponent modelComponent = base.Parent as ModelComponent;
			if (item == null)
			{
				error = SR.Collections_CannotAddANullItem;
				return false;
			}
			if (namedComponent == null || !this.ItemsType.IsAssignableFrom(item.GetType()))
			{
				error = SR.Collections_AddingObjectOfInvalidType(item.GetType().Name);
				return false;
			}
			if (base.Contains(item))
			{
				error = SR.Collections_ItemAlreadyExists;
				return false;
			}
			if (!this.IsValidID(namedComponent.ID, out error))
			{
				return false;
			}
			if (this.ItemsType == typeof(Database) || this.ItemsType.IsSubclassOf(typeof(Database)))
			{
				string text;
				if (!Utils.IsValidNameCharsForDatabase(base.Parent as Server, namedComponent.Name, out text))
				{
					return false;
				}
			}
			else
			{
				if (!this.IsValidName(namedComponent.Name, this.ItemsType, modelComponent.GetModelType(), modelComponent.GetCompatibilityLevel(), out error))
				{
					return false;
				}
				if (!namedComponent.IsCompatible(modelComponent.GetModelType(), modelComponent.GetCompatibilityLevel(), out error))
				{
					return false;
				}
			}
			error = null;
			return true;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00027811 File Offset: 0x00025A11
		protected internal override int Add(ModelComponent item)
		{
			this.ValidateForAddOrInsert(item as NamedComponent);
			return base.Add(item);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00027828 File Offset: 0x00025A28
		private void ValidateForAddOrInsert(NamedComponent obj)
		{
			if (obj == null)
			{
				throw new InvalidOperationException(SR.Collections_CannotAddANullItem);
			}
			if (base.Contains(obj))
			{
				throw new InvalidOperationException(SR.Collections_ItemIsAlreadyInCollectionException(obj.Name, obj.GetType().Name, base.GetType().Name));
			}
			string text;
			if (!this.IsValidID(obj.ID, out text))
			{
				throw new InvalidOperationException(text);
			}
			ModelComponent modelComponent = base.Parent as ModelComponent;
			if (this.ItemsType == typeof(Database) || this.ItemsType.IsSubclassOf(typeof(Database)))
			{
				if (!Utils.IsValidNameCharsForDatabase(base.Parent as Server, obj.Name, out text))
				{
					throw new InvalidOperationException(text);
				}
			}
			else if (!this.IsValidName(obj.Name, this.ItemsType, modelComponent.GetModelType(), modelComponent.GetCompatibilityLevel(), out text))
			{
				throw new InvalidOperationException(text);
			}
			if (modelComponent.GetModelType() != ModelType.Default || modelComponent.GetCompatibilityLevel() > 0)
			{
				obj.ValidateCompatibility(modelComponent.GetModelType(), modelComponent.GetCompatibilityLevel());
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00027932 File Offset: 0x00025B32
		protected internal override void Insert(int index, ModelComponent item)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			this.ValidateForAddOrInsert(item as NamedComponent);
			base.Insert(index, item);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0002796B File Offset: 0x00025B6B
		public bool ContainsName(string name)
		{
			return this.IndexOfName(name) != -1;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002797A File Offset: 0x00025B7A
		public new bool Contains(string id)
		{
			return base.Contains(id);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00027983 File Offset: 0x00025B83
		public string GetNewName()
		{
			return this.GetNewName(this.DefaultNamePrefix);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00027994 File Offset: 0x00025B94
		public string GetNewName(string namePrefix)
		{
			ModelType modelType = (base.Parent as ModelComponent).GetModelType();
			int compatibilityLevel = (base.Parent as ModelComponent).GetCompatibilityLevel();
			namePrefix = ((namePrefix == null) ? this.DefaultNamePrefix : this.GetSyntacticallyValidName(namePrefix, this.ItemsType, modelType, compatibilityLevel));
			if (!this.IsValidName(namePrefix))
			{
				StringGenerator stringGenerator = new StringGenerator(namePrefix, 100);
				for (string text = stringGenerator.Next; text != null; text = stringGenerator.Next)
				{
					if (this.IsValidName(text))
					{
						return text;
					}
				}
				throw new Exception();
			}
			return namePrefix;
		}

		// Token: 0x0600082C RID: 2092
		internal abstract string GetSyntacticallyValidName(string namePrefix, Type type, ModelType modelType, int compatibilityLevel);

		// Token: 0x0600082D RID: 2093 RVA: 0x00027A16 File Offset: 0x00025C16
		public string GetNewID()
		{
			return this.GetNewID(this.DefaultIDPrefix);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00027A24 File Offset: 0x00025C24
		public string GetNewID(string idPrefix)
		{
			if (idPrefix == null)
			{
				idPrefix = this.DefaultIDPrefix;
			}
			else
			{
				idPrefix = this.GetSyntacticallyValidID(idPrefix, this.ItemsType);
			}
			if (!this.IsValidID(idPrefix))
			{
				StringGenerator stringGenerator = new StringGenerator(idPrefix, 100);
				for (string text = stringGenerator.Next; text != null; text = stringGenerator.Next)
				{
					if (this.IsValidID(text))
					{
						return text;
					}
				}
				throw new Exception();
			}
			return idPrefix;
		}

		// Token: 0x0600082F RID: 2095
		internal abstract string GetSyntacticallyValidID(string idPrefix, Type type);

		// Token: 0x06000830 RID: 2096 RVA: 0x00027A84 File Offset: 0x00025C84
		public bool IsValidName(string name)
		{
			string text;
			return this.IsValidName(name, out text);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00027A9A File Offset: 0x00025C9A
		public virtual bool IsValidName(string name, out string error)
		{
			return this.IsValidName(name, this.ItemsType, out error);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00027AAA File Offset: 0x00025CAA
		internal bool IsValidName(string name, Type type, ModelType modelType, int compatibilityLevel, out string error)
		{
			error = null;
			if (!this.IsSyntacticallyValidName(name, type, modelType, compatibilityLevel, out error))
			{
				return false;
			}
			if (this.ContainsName(name))
			{
				error = SR.Collections_NameIsNotUnique(name, this.ItemsType.Name);
				return false;
			}
			return true;
		}

		// Token: 0x06000833 RID: 2099
		internal abstract bool IsSyntacticallyValidName(string name, Type type, ModelType modelType, int compatibilityLevel, out string error);

		// Token: 0x06000834 RID: 2100 RVA: 0x00027AE4 File Offset: 0x00025CE4
		protected bool IsValidName(string name, Type type, out string error)
		{
			error = null;
			ModelComponent modelComponent = base.Parent as ModelComponent;
			if (type == typeof(Database) || type.IsSubclassOf(typeof(Database)))
			{
				string text;
				if (!Utils.IsValidNameCharsForDatabase(base.Parent as Server, name, out text))
				{
					return false;
				}
			}
			else if (!this.IsValidName(name, type, modelComponent.GetModelType(), modelComponent.GetCompatibilityLevel(), out error))
			{
				return false;
			}
			if (this.ContainsName(name))
			{
				error = SR.Collections_NameIsNotUnique(name, this.ItemsType.Name);
				return false;
			}
			return true;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00027B74 File Offset: 0x00025D74
		public bool IsValidID(string id)
		{
			string text;
			return this.IsValidID(id, out text);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00027B8A File Offset: 0x00025D8A
		public virtual bool IsValidID(string id, out string error)
		{
			return this.IsValidID(id, this.ItemsType, out error);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00027B9A File Offset: 0x00025D9A
		protected bool IsValidID(string id, Type type, out string error)
		{
			if (!this.IsSyntacticallyValidID(id, type, out error))
			{
				return false;
			}
			if (this.Contains(id))
			{
				error = SR.Collections_IDIsNotUnique(id, this.ItemsType.Name);
				return false;
			}
			return true;
		}

		// Token: 0x06000838 RID: 2104
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal abstract bool IsSyntacticallyValidID(string id, Type type, out string error);
	}
}
