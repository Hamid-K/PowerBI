using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B4E RID: 2894
	public class Rf2hFolderPropertyCollection : IEnumerable<PropertyValueDefinition>, IEnumerable
	{
		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06005B74 RID: 23412 RVA: 0x00178698 File Offset: 0x00176898
		// (set) Token: 0x06005B75 RID: 23413 RVA: 0x001786A0 File Offset: 0x001768A0
		internal Dictionary<string, PropertyValueDefinition> NamesToProperties { get; private set; }

		// Token: 0x06005B76 RID: 23414 RVA: 0x001786A9 File Offset: 0x001768A9
		internal Rf2hFolderPropertyCollection(Rf2hFolderWithFieldsAndProperties parent)
		{
			this.parent = parent;
			this.NamesToProperties = new Dictionary<string, PropertyValueDefinition>();
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x001786C3 File Offset: 0x001768C3
		public IEnumerator<PropertyValueDefinition> GetEnumerator()
		{
			this.EnsurePropertyCollectionExists();
			return new Rf2hFolderPropertyCollectionEnumerator(this);
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x001786D1 File Offset: 0x001768D1
		private IEnumerator GetEnumerator1()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x001786D9 File Offset: 0x001768D9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator1();
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06005B7A RID: 23418 RVA: 0x001786E1 File Offset: 0x001768E1
		public int Count
		{
			get
			{
				this.EnsurePropertyCollectionExists();
				return this.NamesToProperties.Count;
			}
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x001786F4 File Offset: 0x001768F4
		public void Add(PropertyValueDefinition newPropertyValue)
		{
			if (newPropertyValue == null)
			{
				throw new ArgumentNullException("newPropertyValue");
			}
			if (newPropertyValue.Parent != null)
			{
				if (newPropertyValue.Parent == this)
				{
					throw new CustomMqClientException(SR.PropertyAlreadyInFolder);
				}
				throw new CustomMqClientException(SR.PropertyOneFolder);
			}
			else
			{
				if (newPropertyValue.InsertIntoFolderType != this.parent.FolderType)
				{
					throw new CustomMqClientException(SR.AddPropertyWrongFolderType);
				}
				if (newPropertyValue.InsertIntoFolderType == Rf2hFolderType.Properties && newPropertyValue.InsertIntoPropertiesFolderTag != this.parent.FolderTag)
				{
					throw new CustomMqClientException(SR.AddPropertyWrongFolderTag);
				}
				this.EnsurePropertyCollectionExists();
				string fullName = newPropertyValue.FullName;
				foreach (string text in this.NamesToProperties.Keys)
				{
					if (fullName == text)
					{
						throw new CustomMqClientException(SR.PropertiesWithSameName);
					}
					if (fullName.StartsWith(text) && fullName[text.Length] == '.')
					{
						throw new CustomMqClientException(SR.MixedModeOccurs);
					}
					if (text.StartsWith(fullName) && text[fullName.Length] == '.')
					{
						throw new CustomMqClientException(SR.MixedModeOccurs);
					}
				}
				this.NamesToProperties.Add(newPropertyValue.FullName, newPropertyValue);
				newPropertyValue.Parent = this;
				this.PropertyCollectionUpdated();
				return;
			}
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x0017884C File Offset: 0x00176A4C
		public bool Remove(PropertyValueDefinition oldPropertyValue)
		{
			if (oldPropertyValue == null)
			{
				throw new ArgumentNullException("oldPropertyValue");
			}
			if (oldPropertyValue.Parent == null)
			{
				return false;
			}
			if (oldPropertyValue.Parent != this)
			{
				return false;
			}
			this.EnsurePropertyCollectionExists();
			if (!this.NamesToProperties.Remove(oldPropertyValue.FullName))
			{
				throw new InvalidOperationException("BUGBUG: couldn't find property to remove");
			}
			oldPropertyValue.Parent = null;
			this.PropertyCollectionUpdated();
			return true;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x001788B0 File Offset: 0x00176AB0
		public void Clear()
		{
			if (this.NamesToProperties.Count != 0)
			{
				foreach (KeyValuePair<string, PropertyValueDefinition> keyValuePair in this.NamesToProperties)
				{
					keyValuePair.Value.Parent = null;
				}
				this.NamesToProperties.Clear();
				this.PropertyCollectionUpdated();
			}
		}

		// Token: 0x17001619 RID: 5657
		public PropertyValueDefinition this[string propertyName]
		{
			get
			{
				if (string.IsNullOrWhiteSpace(propertyName))
				{
					throw new ArgumentNullException("propertyName");
				}
				this.EnsurePropertyCollectionExists();
				PropertyValueDefinition propertyValueDefinition;
				if (!this.NamesToProperties.TryGetValue(propertyName, out propertyValueDefinition))
				{
					throw new CustomMqClientException(SR.PropertyNotFoundInFolder);
				}
				return propertyValueDefinition;
			}
			set
			{
				if (string.IsNullOrWhiteSpace(propertyName))
				{
					throw new ArgumentNullException("propertyName");
				}
				if (propertyName != value.FullName)
				{
					throw new CustomMqClientException(SR.PropertyNameAndValueNameDiffer);
				}
				this.EnsurePropertyCollectionExists();
				PropertyValueDefinition propertyValueDefinition;
				if (this.NamesToProperties.TryGetValue(propertyName, out propertyValueDefinition))
				{
					this.Remove(propertyValueDefinition);
				}
				this.Add(value);
			}
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x001789CA File Offset: 0x00176BCA
		public bool TryGetValue(string propertyName, out PropertyValueDefinition propertyValue)
		{
			if (string.IsNullOrWhiteSpace(propertyName))
			{
				throw new ArgumentNullException("propertyName");
			}
			this.EnsurePropertyCollectionExists();
			return this.NamesToProperties.TryGetValue(propertyName, out propertyValue);
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x001789F2 File Offset: 0x00176BF2
		private void EnsurePropertyCollectionExists()
		{
			this.parent.EnsurePropertyCollectionExists();
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x001789FF File Offset: 0x00176BFF
		private void PropertyCollectionUpdated()
		{
			this.parent.PropertyCollectionUpdated();
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x00178A0C File Offset: 0x00176C0C
		internal void PropertyValueUpdated()
		{
			this.PropertyCollectionUpdated();
		}

		// Token: 0x040047F7 RID: 18423
		private Rf2hFolderWithFieldsAndProperties parent;
	}
}
