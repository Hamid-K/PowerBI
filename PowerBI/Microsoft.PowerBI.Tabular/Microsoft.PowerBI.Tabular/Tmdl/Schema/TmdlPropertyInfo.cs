using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x0200015A RID: 346
	[DebuggerDisplay("TmdlPropertyInfo - name={Name}, type={Type}")]
	internal sealed class TmdlPropertyInfo : TmdlSchemaElement, ITmdlPropertiesContainer
	{
		// Token: 0x060015D1 RID: 5585 RVA: 0x00091E54 File Offset: 0x00090054
		public TmdlPropertyInfo(string name, bool isDefaultProperty = false)
			: this(name, TmdlValueType.String, null, null, null, null, null, isDefaultProperty, false)
		{
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00091E8C File Offset: 0x0009008C
		public TmdlPropertyInfo(string name, TmdlValueType type, TmdlScalarValueType? scalarValueType = null, TmdlExpressionLanguage? expressionLanguage = null, Type enumType = null, ObjectType? metadataObjectType = null, TmdlTranslationElementInfo rootElementInfo = null, bool isDefaultProperty = false, bool canBeDuplicated = false)
		{
			this.Name = name;
			this.Type = type;
			this.ScalarValueType = scalarValueType;
			this.ExpressionLanguage = expressionLanguage;
			this.EnumType = enumType;
			this.IsDefaultProperty = isDefaultProperty;
			this.CanBeDuplicated = canBeDuplicated;
			this.MetadataObjectType = metadataObjectType;
			this.RootElementInfo = rootElementInfo;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00091EE4 File Offset: 0x000900E4
		public string Name { get; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00091EEC File Offset: 0x000900EC
		public TmdlValueType Type { get; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00091EF4 File Offset: 0x000900F4
		public TmdlScalarValueType? ScalarValueType { get; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00091EFC File Offset: 0x000900FC
		public Type EnumType { get; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00091F04 File Offset: 0x00090104
		public bool IsDefaultProperty { get; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00091F0C File Offset: 0x0009010C
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00091F14 File Offset: 0x00090114
		public bool CanBeDuplicated
		{
			get
			{
				return this.canBeDuplicated;
			}
			internal set
			{
				base.EnsureNotReadOnly();
				this.canBeDuplicated = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00091F23 File Offset: 0x00090123
		public ObjectType? MetadataObjectType { get; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00091F2B File Offset: 0x0009012B
		public TmdlTranslationElementInfo RootElementInfo { get; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00091F33 File Offset: 0x00090133
		public TmdlExpressionLanguage? ExpressionLanguage { get; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x00091F3B File Offset: 0x0009013B
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x00091F43 File Offset: 0x00090143
		public bool IsDeprecated { get; private set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x00091F4C File Offset: 0x0009014C
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x00091F54 File Offset: 0x00090154
		public string DeprecationMessage { get; private set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x00091F60 File Offset: 0x00090160
		public IReadOnlyCollection<TmdlPropertyInfo> Children
		{
			get
			{
				IReadOnlyCollection<TmdlPropertyInfo> readOnlyCollection = this.children;
				return readOnlyCollection ?? TmdlPropertyInfo.EmptyPropertyList;
			}
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00091F80 File Offset: 0x00090180
		public TmdlPropertyInfo FindProperty(string propertyName)
		{
			if (this.children == null)
			{
				return null;
			}
			return this.children.FirstOrDefault((TmdlPropertyInfo x) => propertyName.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00091FBB File Offset: 0x000901BB
		public bool TryGetPropertyInfo(string keyword, out TmdlPropertyInfo info)
		{
			info = this.FindProperty(keyword);
			return info != null;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00091FCB File Offset: 0x000901CB
		internal void MarkAsDeprecated(string message)
		{
			base.EnsureNotReadOnly();
			this.IsDeprecated = true;
			this.DeprecationMessage = message;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00091FE1 File Offset: 0x000901E1
		internal void AddChildProperty(TmdlPropertyInfo property)
		{
			base.EnsureNotReadOnly();
			if (this.children == null)
			{
				this.children = new List<TmdlPropertyInfo>();
			}
			this.children.Add(property);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00092008 File Offset: 0x00090208
		internal bool RemoveChildProperty(TmdlPropertyInfo property)
		{
			base.EnsureNotReadOnly();
			return this.children != null && this.children.Remove(property);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00092026 File Offset: 0x00090226
		internal void ClearChildProperties()
		{
			base.EnsureNotReadOnly();
			if (this.children != null)
			{
				this.children.Clear();
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00092044 File Offset: 0x00090244
		internal TmdlPropertyInfo Clone(bool makeReadOnly = false)
		{
			TmdlPropertyInfo tmdlPropertyInfo;
			if (base.IsReadOnly && makeReadOnly)
			{
				tmdlPropertyInfo = new TmdlPropertyInfo(this.Name, this.Type, this.ScalarValueType, this.ExpressionLanguage, this.EnumType, this.MetadataObjectType, this.RootElementInfo, this.IsDefaultProperty, this.CanBeDuplicated);
				tmdlPropertyInfo.children = this.children;
			}
			else
			{
				TmdlTranslationElementInfo tmdlTranslationElementInfo = ((this.RootElementInfo != null) ? this.RootElementInfo.Clone(makeReadOnly) : null);
				tmdlPropertyInfo = new TmdlPropertyInfo(this.Name, this.Type, this.ScalarValueType, this.ExpressionLanguage, this.EnumType, this.MetadataObjectType, tmdlTranslationElementInfo, this.IsDefaultProperty, this.CanBeDuplicated);
				if (this.children != null)
				{
					tmdlPropertyInfo.children = new List<TmdlPropertyInfo>(this.children.Count);
					for (int i = 0; i < this.children.Count; i++)
					{
						if (!makeReadOnly || !this.children[i].IsReadOnly)
						{
							tmdlPropertyInfo.children.Add(this.children[i].Clone(makeReadOnly));
						}
						else
						{
							tmdlPropertyInfo.children.Add(this.children[i]);
						}
					}
				}
			}
			if (makeReadOnly)
			{
				tmdlPropertyInfo.MakeReadOnly();
			}
			return tmdlPropertyInfo;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00092184 File Offset: 0x00090384
		private protected override void MakeReadOnlyImpl()
		{
			if (this.RootElementInfo != null && !this.RootElementInfo.IsReadOnly)
			{
				this.RootElementInfo.MakeReadOnly();
			}
			if (this.children != null)
			{
				foreach (TmdlPropertyInfo tmdlPropertyInfo in this.children.Where((TmdlPropertyInfo p) => !p.IsReadOnly))
				{
					tmdlPropertyInfo.MakeReadOnly();
				}
			}
		}

		// Token: 0x040003F1 RID: 1009
		internal static readonly IReadOnlyCollection<TmdlPropertyInfo> EmptyPropertyList = new List<TmdlPropertyInfo>(0);

		// Token: 0x040003F2 RID: 1010
		private bool canBeDuplicated;

		// Token: 0x040003F3 RID: 1011
		private List<TmdlPropertyInfo> children;
	}
}
