using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000F0 RID: 240
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ProcessingRule : Rule
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C34 RID: 3124
		public abstract int ProcessOnPass { get; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x000283F7 File Offset: 0x000265F7
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x000283FF File Offset: 0x000265FF
		public bool MakeFriendlyName
		{
			get
			{
				return this.m_makeFriendlyName;
			}
			set
			{
				this.m_makeFriendlyName = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00028408 File Offset: 0x00026608
		public RenamerCollection Renamers
		{
			get
			{
				return this.m_renamers;
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00028410 File Offset: 0x00026610
		public override bool AppliesTo(DsvItem dsvItem)
		{
			return (!(dsvItem is DsvTable) || this is ITableProcessingRule) && (!(dsvItem is DsvColumn) || this is IColumnProcessingRule) && (!(dsvItem is DsvRelation) || this is IRelationProcessingRule) && base.AppliesTo(dsvItem);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00028450 File Offset: 0x00026650
		internal RuleProcessResult Process(DsvItem dsvItem)
		{
			if (base.BindingContext == null)
			{
				throw new InvalidOperationException();
			}
			DsvTable dsvTable = dsvItem as DsvTable;
			DsvColumn dsvColumn = dsvItem as DsvColumn;
			DsvRelation dsvRelation = dsvItem as DsvRelation;
			if (dsvTable != null)
			{
				return ((ITableProcessingRule)this).Process(dsvTable, base.BindingContext.GetBindingInfo(dsvTable));
			}
			if (dsvColumn != null)
			{
				return ((IColumnProcessingRule)this).Process(dsvColumn, base.BindingContext.GetBindingInfo(dsvColumn));
			}
			if (dsvRelation != null)
			{
				return ((IRelationProcessingRule)this).Process(dsvRelation, base.BindingContext.GetBindingInfo(dsvRelation));
			}
			string text = "Unhandled DsvItem type ";
			Type type = dsvItem.GetType();
			throw new InternalModelingException(text + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000284F5 File Offset: 0x000266F5
		internal RuleProcessResult ProcessFailed(params string[] messages)
		{
			return new RuleProcessResult(false, null, null, messages);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00028500 File Offset: 0x00026700
		internal RuleProcessResult ProcessSkipped(params string[] messages)
		{
			return new RuleProcessResult(true, null, null, messages);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002850B File Offset: 0x0002670B
		internal RuleProcessResult ProcessFoundExistingModelItem(ModelItem item)
		{
			return this.ProcessSkipped(new string[] { SR.Rules_FoundExistingModelItem(SRObjectDescriptor.FromScope(item)) });
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00028527 File Offset: 0x00026727
		internal RuleProcessResult ProcessDependentRulesSkipped(ModelItem item)
		{
			return this.ProcessSkipped(new string[] { SR.Rules_DependentRuleOnExistingItem(SRObjectDescriptor.FromScope(item)) });
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00028543 File Offset: 0x00026743
		internal RuleProcessResult ProcessCreatedModelItem(ModelItem createdItem)
		{
			return this.ProcessCreatedModelItem(createdItem, null);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0002854D File Offset: 0x0002674D
		internal RuleProcessResult ProcessCreatedModelItem(ModelItem createdItem, ModelItem existingItem)
		{
			return new RuleProcessResult(true, createdItem, existingItem, SR.Rules_CreatedModelItem(SRObjectDescriptor.FromScope(createdItem)));
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00028564 File Offset: 0x00026764
		internal RuleProcessResult ProcessCreatedModelItems(IList<ModelItem> createdItems)
		{
			string[] array = new string[createdItems.Count];
			for (int i = 0; i < createdItems.Count; i++)
			{
				array[i] = SR.Rules_CreatedModelItem(SRObjectDescriptor.FromScope(createdItems[i]));
			}
			return new RuleProcessResult(true, createdItems, null, array);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000285AB File Offset: 0x000267AB
		internal RuleProcessResult ProcessCreatedModelItems(IList<ModelItem> createdItems, bool messageForFirstItemOnly)
		{
			if (messageForFirstItemOnly)
			{
				return new RuleProcessResult(true, createdItems, null, new string[] { SR.Rules_CreatedModelItem(SRObjectDescriptor.FromScope(createdItems[0])) });
			}
			return this.ProcessCreatedModelItems(createdItems);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000285DA File Offset: 0x000267DA
		internal RuleProcessResult ProcessModifiedItem(ModelItem modifiedItem)
		{
			return new RuleProcessResult(true, null, modifiedItem, SR.Rules_ModifiedModelItem(SRObjectDescriptor.FromScope(modifiedItem)));
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000285EF File Offset: 0x000267EF
		internal void FinalizeModelItem(ModelItem item, IXPathNavigable fragment, IXPathNavigable folderFragment)
		{
			if (fragment != null)
			{
				item.LoadFragment(fragment);
			}
			if (folderFragment != null)
			{
				this.MoveToFolder(item, folderFragment);
			}
			this.FixNameCollisions(item);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002860D File Offset: 0x0002680D
		internal string CreateModelItemName(DsvItem dsvItem)
		{
			return this.CreateModelItemName(this.GetDsvItemName(dsvItem));
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002861C File Offset: 0x0002681C
		internal string CreateModelItemName(string nameSource)
		{
			string text = this.ApplyRenamers(nameSource);
			if (this.m_makeFriendlyName)
			{
				text = this.MakeFriendlyNameString(text);
			}
			return text;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00028644 File Offset: 0x00026844
		internal string ApplyRenamers(string name)
		{
			using (List<Renamer>.Enumerator enumerator = this.m_renamers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text;
					if (enumerator.Current.Rename(name, out text))
					{
						return text;
					}
				}
			}
			return name;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000286A0 File Offset: 0x000268A0
		internal void FixNameCollisions(ModelItem item)
		{
			if (item == null)
			{
				throw new InternalModelingException("item is null");
			}
			List<ModelItem> list = new List<ModelItem>(item.GetNamespaceItems());
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != item && ModelItem.CompareNames(list[i].Name, item.Name) == 0)
				{
					item.Name = StringManipulation.IncrementDigitSuffix(item.Name);
					i = 0;
				}
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00028710 File Offset: 0x00026910
		internal void ReplaceStringTokens(IXPathNavigable fragment, params KeyValuePair<string, string>[] tokenReplacements)
		{
			foreach (object obj in fragment.CreateNavigator().SelectDescendants(XPathNodeType.Text, true))
			{
				XPathNavigator xpathNavigator = (XPathNavigator)obj;
				foreach (KeyValuePair<string, string> keyValuePair in tokenReplacements)
				{
					if (xpathNavigator.Value.Contains(keyValuePair.Key))
					{
						xpathNavigator.SetValue(xpathNavigator.Value.Replace(keyValuePair.Key, keyValuePair.Value));
					}
				}
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000287B8 File Offset: 0x000269B8
		internal void ReplaceIdTokens(IXPathNavigable fragment, params KeyValuePair<string, QName>[] tokenReplacements)
		{
			foreach (object obj in fragment.CreateNavigator().SelectDescendants(XPathNodeType.Text, true))
			{
				XPathNavigator xpathNavigator = (XPathNavigator)obj;
				foreach (KeyValuePair<string, QName> keyValuePair in tokenReplacements)
				{
					if (xpathNavigator.Value == keyValuePair.Key)
					{
						XmlQualifiedName xmlQualifiedName = ModelItem.GetXmlQualifiedName(keyValuePair.Value);
						string text = (string)XmlSchemaType.GetBuiltInSimpleType(XmlTypeCode.QName).Datatype.ChangeType(xmlQualifiedName, typeof(string), xpathNavigator);
						xpathNavigator.SetValue(xpathNavigator.Value.Replace(keyValuePair.Key, text));
					}
				}
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00028894 File Offset: 0x00026A94
		internal void InsertItemSortedByName(ModelItem item, IOwnedModelItemCollection coll)
		{
			int num = 0;
			for (int i = coll.Count - 1; i >= 0; i--)
			{
				if (ModelItem.CompareNames(item.Name, coll[i].Name) >= 0)
				{
					num = i + 1;
					break;
				}
			}
			coll.Insert(num, item);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000288E0 File Offset: 0x00026AE0
		internal void MoveFieldSortedByOrdinal(ModelField field)
		{
			if (field.OwnerCollection == null)
			{
				throw new InternalModelingException("field must have an OwnerCollection");
			}
			int ordinalFromField = ProcessingRule.GetOrdinalFromField(field);
			if (ordinalFromField < 0)
			{
				return;
			}
			IOwnedModelItemCollection ownerCollection = field.OwnerCollection;
			ownerCollection.Remove(field);
			int num = ownerCollection.Count;
			for (int i = ownerCollection.Count - 1; i >= 0; i--)
			{
				int ordinalFromField2 = ProcessingRule.GetOrdinalFromField(ownerCollection[i] as ModelField);
				if (ordinalFromField2 >= 0)
				{
					if (ordinalFromField >= ordinalFromField2)
					{
						num = i + 1;
						break;
					}
					num = i;
				}
			}
			ownerCollection.Insert(num, field);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00028964 File Offset: 0x00026B64
		internal void MoveToFolder(ModelItem item, IXPathNavigable folderFragment)
		{
			if (item is ModelEntityFolderItem)
			{
				this.MoveToFolderCore<ModelEntityFolder>(item, folderFragment);
				return;
			}
			if (item is ModelFieldFolderItem)
			{
				this.MoveToFolderCore<ModelFieldFolder>(item, folderFragment);
				return;
			}
			string text = "Unhandled item type: ";
			Type type = item.GetType();
			throw new InternalModelingException(text + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000289B4 File Offset: 0x00026BB4
		private void MoveToFolderCore<F>(ModelItem item, IXPathNavigable folderFragment) where F : ModelItem, IFolderItem, new()
		{
			if (item == null || folderFragment == null)
			{
				throw new InternalModelingException("item or folderFragment is null");
			}
			if (item.OwnerCollection == null)
			{
				throw new InternalModelingException("item.OwnerCollection is null");
			}
			F f = new F();
			if (!item.OwnerCollection.CanContain(f))
			{
				string text = "Cannot create folder as child of ";
				ModelItem parentItem = item.ParentItem;
				throw new InternalModelingException(text + ((parentItem != null) ? parentItem.ToString() : null));
			}
			f.LoadFragment(folderFragment);
			if (f.Name.Length == 0)
			{
				throw new InternalModelingException("Folder name is empty");
			}
			ModelItem modelItem = item.OwnerCollection[f.Name];
			if (modelItem is F)
			{
				f = (F)((object)modelItem);
			}
			else
			{
				this.InsertItemSortedByName(f, item.OwnerCollection);
				if (modelItem != null)
				{
					this.FixNameCollisions(f);
				}
			}
			if (!f.Items.CanContain(item))
			{
				string text2 = "Folder/item mismatch (";
				Type type = f.GetType();
				string text3 = ((type != null) ? type.ToString() : null);
				string text4 = " cannot contain ";
				Type type2 = item.GetType();
				throw new InternalModelingException(text2 + text3 + text4 + ((type2 != null) ? type2.ToString() : null));
			}
			item.OwnerCollection.Remove(item);
			f.Items.Add(item);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00028B04 File Offset: 0x00026D04
		private static int GetOrdinalFromField(ModelField field)
		{
			ModelAttribute modelAttribute = field as ModelAttribute;
			ModelRole modelRole = field as ModelRole;
			DsvColumn dsvColumn = null;
			if (modelAttribute != null && modelAttribute.Binding != null)
			{
				dsvColumn = modelAttribute.Binding.GetColumn();
			}
			if (modelRole != null)
			{
				if (modelRole.Binding != null)
				{
					DsvRelation relation = modelRole.Binding.GetRelation();
					if (relation != null)
					{
						if (modelRole.Binding.RelationEnd == RelationEnd.Source)
						{
							dsvColumn = relation.TargetColumns[0];
						}
						else
						{
							dsvColumn = relation.SourceColumns[0];
						}
					}
				}
				else if (modelRole.RelatedEntity != null && modelRole.RelatedEntity.Binding is ColumnBinding)
				{
					dsvColumn = ((ColumnBinding)modelRole.RelatedEntity.Binding).GetColumn();
				}
			}
			if (dsvColumn != null)
			{
				return dsvColumn.Ordinal;
			}
			return -1;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00028BB8 File Offset: 0x00026DB8
		private string GetDsvItemName(DsvItem dsvItem)
		{
			string text = null;
			if (dsvItem is IHasFriendlyName)
			{
				text = ((IHasFriendlyName)dsvItem).FriendlyName;
			}
			if (string.IsNullOrEmpty(text))
			{
				DsvTable dsvTable = dsvItem as DsvTable;
				DsvColumn dsvColumn = dsvItem as DsvColumn;
				if (dsvTable != null)
				{
					text = ((dsvTable.DbSchemaName.Length > 0) ? (dsvTable.DbSchemaName + ".") : "") + dsvTable.DbTableName;
				}
				else if (dsvColumn != null)
				{
					text = dsvColumn.DbColumnName;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = dsvItem.Name;
			}
			return text;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00028C44 File Offset: 0x00026E44
		private string MakeFriendlyNameString(string name)
		{
			TextInfo textInfo = base.StringCulture.TextInfo;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			bool flag2 = false;
			int i = 0;
			while (i < name.Length)
			{
				char c = name[i];
				bool flag3 = i + 1 < name.Length && char.IsLower(name[i + 1]);
				if (flag)
				{
					if (!this.IsSpacingChar(c))
					{
						flag = false;
						goto IL_0096;
					}
				}
				else
				{
					if (this.ConvertWordSeparator(ref c))
					{
						flag = true;
						goto IL_0096;
					}
					if ((!char.IsUpper(c) && !char.IsDigit(c)) || (!flag2 && !flag3))
					{
						goto IL_0096;
					}
					stringBuilder.Append(' ');
					if (flag)
					{
						throw new InternalModelingException("Unexpected word boundary at start of word");
					}
					goto IL_0096;
				}
				IL_00A7:
				i++;
				continue;
				IL_0096:
				flag2 = char.IsLower(c);
				stringBuilder.Append(c);
				goto IL_00A7;
			}
			return textInfo.ToTitleCase(stringBuilder.ToString()).Trim();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00028D1C File Offset: 0x00026F1C
		private bool IsSpacingChar(char c)
		{
			UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00028D3C File Offset: 0x00026F3C
		private bool ConvertWordSeparator(ref char c)
		{
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.SpaceSeparator:
			case UnicodeCategory.OpenPunctuation:
			case UnicodeCategory.InitialQuotePunctuation:
				return true;
			case UnicodeCategory.LineSeparator:
			case UnicodeCategory.ParagraphSeparator:
			case UnicodeCategory.ConnectorPunctuation:
			case UnicodeCategory.DashPunctuation:
				c = ' ';
				return true;
			}
			return false;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00028D95 File Offset: 0x00026F95
		internal override bool LoadXmlAttribute(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "makeFriendlyName")
			{
				this.m_makeFriendlyName = xr.ReadValueAsBoolean();
				return true;
			}
			return base.LoadXmlAttribute(xr, objectFactory);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00028DC7 File Offset: 0x00026FC7
		internal override bool LoadXmlElement(ModelingXmlReader xr, IXmlObjectFactory objectFactory)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "renamer")
			{
				this.m_renamers.Add(Renamer.FromReader(xr));
				return true;
			}
			return base.LoadXmlElement(xr, objectFactory);
		}

		// Token: 0x0400050E RID: 1294
		private const string MakeFriendlyNameAttr = "makeFriendlyName";

		// Token: 0x0400050F RID: 1295
		private bool m_makeFriendlyName = true;

		// Token: 0x04000510 RID: 1296
		private readonly RenamerCollection m_renamers = new RenamerCollection();
	}
}
