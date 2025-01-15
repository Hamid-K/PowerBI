using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x0200015F RID: 351
	[DebuggerDisplay("TmdlTranslationElementInfo - type={ObjectType}")]
	internal sealed class TmdlTranslationElementInfo : TmdlSchemaElement, ITmdlTranslationElementContainer, ITmdlPropertiesContainer
	{
		// Token: 0x06001611 RID: 5649 RVA: 0x00093698 File Offset: 0x00091898
		public TmdlTranslationElementInfo(ObjectType objectType)
		{
			this.ObjectType = objectType;
			this.RequiresName = ObjectTreeHelper.IsNamedObject(this.ObjectType) || (ObjectTreeHelper.IsKeyedObject(this.ObjectType) && this.ObjectType != ObjectType.CalculationExpression);
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x000936E5 File Offset: 0x000918E5
		public ObjectType ObjectType { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x000936ED File Offset: 0x000918ED
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x000936F5 File Offset: 0x000918F5
		public bool IsSingleton
		{
			get
			{
				return this.isSingleton;
			}
			set
			{
				base.EnsureNotReadOnly();
				this.isSingleton = value;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00093704 File Offset: 0x00091904
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x0009370C File Offset: 0x0009190C
		public TmdlPropertyInfo NameProperty
		{
			get
			{
				return this.nameProperty;
			}
			set
			{
				base.EnsureNotReadOnly();
				this.nameProperty = value;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0009371C File Offset: 0x0009191C
		public IReadOnlyCollection<TmdlPropertyInfo> Properties
		{
			get
			{
				IReadOnlyCollection<TmdlPropertyInfo> readOnlyCollection = this.properties;
				return readOnlyCollection ?? TmdlPropertyInfo.EmptyPropertyList;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0009373C File Offset: 0x0009193C
		public IReadOnlyCollection<TmdlTranslationElementInfo> ChildElements
		{
			get
			{
				IReadOnlyCollection<TmdlTranslationElementInfo> readOnlyCollection = this.childElements;
				return readOnlyCollection ?? TmdlTranslationElementInfo.EmptyTranslationElementList;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0009375A File Offset: 0x0009195A
		internal bool RequiresName { get; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x00093762 File Offset: 0x00091962
		internal bool IsEmpty
		{
			get
			{
				return (this.properties == null || this.properties.Count <= 0) && (this.childElements == null || this.childElements.Count <= 0);
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00093798 File Offset: 0x00091998
		public TmdlPropertyInfo FindProperty(string propertyName)
		{
			if (this.properties == null)
			{
				return null;
			}
			return this.properties.FirstOrDefault((TmdlPropertyInfo x) => propertyName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x000937D3 File Offset: 0x000919D3
		public bool TryGetPropertyInfo(string keyword, out TmdlPropertyInfo info)
		{
			info = this.FindProperty(keyword);
			return info != null;
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000937E4 File Offset: 0x000919E4
		public bool TryGetTranslationElementInfo(string keyword, out TmdlTranslationElementInfo info)
		{
			if (this.childElements == null)
			{
				info = null;
			}
			else
			{
				info = this.childElements.FirstOrDefault((TmdlTranslationElementInfo x) => keyword.Equals(x.ObjectType.ToString(), StringComparison.InvariantCultureIgnoreCase));
			}
			return info != null;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00093829 File Offset: 0x00091A29
		internal void AddProperty(TmdlPropertyInfo property)
		{
			base.EnsureNotReadOnly();
			if (this.properties == null)
			{
				this.properties = new List<TmdlPropertyInfo>();
			}
			this.properties.Add(property);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00093850 File Offset: 0x00091A50
		internal bool RemoveProperty(TmdlPropertyInfo property)
		{
			base.EnsureNotReadOnly();
			return this.properties != null && this.properties.Remove(property);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0009386E File Offset: 0x00091A6E
		internal void ClearProperties()
		{
			base.EnsureNotReadOnly();
			if (this.properties != null)
			{
				this.properties.Clear();
			}
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00093889 File Offset: 0x00091A89
		internal void AddChildElement(TmdlTranslationElementInfo element)
		{
			base.EnsureNotReadOnly();
			if (this.childElements == null)
			{
				this.childElements = new List<TmdlTranslationElementInfo>();
			}
			this.childElements.Add(element);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000938B0 File Offset: 0x00091AB0
		internal bool RemoveChildElement(TmdlTranslationElementInfo element)
		{
			base.EnsureNotReadOnly();
			return this.childElements != null && this.childElements.Remove(element);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x000938CE File Offset: 0x00091ACE
		internal void ClearChildElements()
		{
			base.EnsureNotReadOnly();
			if (this.childElements != null)
			{
				this.childElements.Clear();
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000938EC File Offset: 0x00091AEC
		internal TmdlTranslationElementInfo Clone(bool makeReadOnly = false)
		{
			TmdlTranslationElementInfo tmdlTranslationElementInfo = new TmdlTranslationElementInfo(this.ObjectType)
			{
				IsSingleton = this.IsSingleton
			};
			if (base.IsReadOnly && makeReadOnly)
			{
				tmdlTranslationElementInfo.nameProperty = this.nameProperty;
				tmdlTranslationElementInfo.properties = this.properties;
				tmdlTranslationElementInfo.childElements = this.childElements;
			}
			else
			{
				if (this.nameProperty != null)
				{
					if (!makeReadOnly || !this.nameProperty.IsReadOnly)
					{
						tmdlTranslationElementInfo.nameProperty = this.nameProperty.Clone(makeReadOnly);
					}
					else
					{
						tmdlTranslationElementInfo.nameProperty = this.nameProperty;
					}
				}
				if (this.properties != null)
				{
					tmdlTranslationElementInfo.properties = new List<TmdlPropertyInfo>(this.properties.Count);
					for (int i = 0; i < this.properties.Count; i++)
					{
						if (!makeReadOnly || !this.properties[i].IsReadOnly)
						{
							tmdlTranslationElementInfo.properties.Add(this.properties[i].Clone(makeReadOnly));
						}
						else
						{
							tmdlTranslationElementInfo.properties.Add(this.properties[i]);
						}
					}
				}
				if (this.childElements != null)
				{
					tmdlTranslationElementInfo.childElements = new List<TmdlTranslationElementInfo>(this.childElements.Count);
					for (int j = 0; j < this.childElements.Count; j++)
					{
						if (!makeReadOnly || !this.childElements[j].IsReadOnly)
						{
							tmdlTranslationElementInfo.childElements.Add(this.childElements[j].Clone(makeReadOnly));
						}
						else
						{
							tmdlTranslationElementInfo.childElements.Add(this.childElements[j]);
						}
					}
				}
			}
			if (makeReadOnly)
			{
				tmdlTranslationElementInfo.MakeReadOnly();
			}
			return tmdlTranslationElementInfo;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00093A88 File Offset: 0x00091C88
		private protected override void MakeReadOnlyImpl()
		{
			if (this.nameProperty != null && !this.nameProperty.IsReadOnly)
			{
				this.nameProperty.MakeReadOnly();
			}
			if (this.properties != null)
			{
				foreach (TmdlPropertyInfo tmdlPropertyInfo in this.properties.Where((TmdlPropertyInfo p) => !p.IsReadOnly))
				{
					tmdlPropertyInfo.MakeReadOnly();
				}
			}
			if (this.childElements != null)
			{
				foreach (TmdlTranslationElementInfo tmdlTranslationElementInfo in this.childElements.Where((TmdlTranslationElementInfo c) => !c.IsReadOnly))
				{
					tmdlTranslationElementInfo.MakeReadOnly();
				}
			}
		}

		// Token: 0x0400040A RID: 1034
		private static readonly IReadOnlyCollection<TmdlTranslationElementInfo> EmptyTranslationElementList = new List<TmdlTranslationElementInfo>(0);

		// Token: 0x0400040B RID: 1035
		private bool isSingleton;

		// Token: 0x0400040C RID: 1036
		private TmdlPropertyInfo nameProperty;

		// Token: 0x0400040D RID: 1037
		private List<TmdlPropertyInfo> properties;

		// Token: 0x0400040E RID: 1038
		private List<TmdlTranslationElementInfo> childElements;
	}
}
