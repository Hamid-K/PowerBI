using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003DC RID: 988
	public sealed class Grouping
	{
		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0007E613 File Offset: 0x0007C813
		// (set) Token: 0x06001F75 RID: 8053 RVA: 0x0007E61B File Offset: 0x0007C81B
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0007E624 File Offset: 0x0007C824
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x0007E62C File Offset: 0x0007C82C
		[DefaultValue(typeof(Expression), "")]
		public Expression Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x0007E635 File Offset: 0x0007C835
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x0007E63D File Offset: 0x0007C83D
		public GroupExpressions GroupExpressions
		{
			get
			{
				return this.m_groupExpressions;
			}
			set
			{
				this.m_groupExpressions = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x0007E646 File Offset: 0x0007C846
		// (set) Token: 0x06001F7B RID: 8059 RVA: 0x0007E64E File Offset: 0x0007C84E
		[DefaultValue(false)]
		public bool PageBreakAtStart
		{
			get
			{
				return this.m_pageBreakAtStart;
			}
			set
			{
				this.m_pageBreakAtStart = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x0007E657 File Offset: 0x0007C857
		// (set) Token: 0x06001F7D RID: 8061 RVA: 0x0007E65F File Offset: 0x0007C85F
		[DefaultValue(false)]
		public bool PageBreakAtEnd
		{
			get
			{
				return this.m_pageBreakAtEnd;
			}
			set
			{
				this.m_pageBreakAtEnd = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x0007E668 File Offset: 0x0007C868
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x0007E670 File Offset: 0x0007C870
		[DefaultValue(false)]
		public bool naturalGroup
		{
			get
			{
				return this.m_naturalGroup;
			}
			set
			{
				this.m_naturalGroup = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0007E679 File Offset: 0x0007C879
		// (set) Token: 0x06001F81 RID: 8065 RVA: 0x0007E681 File Offset: 0x0007C881
		public Filters Filters
		{
			get
			{
				return this.m_filters;
			}
			set
			{
				this.m_filters = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0007E68A File Offset: 0x0007C88A
		// (set) Token: 0x06001F83 RID: 8067 RVA: 0x0007E692 File Offset: 0x0007C892
		[DefaultValue(typeof(Expression), "")]
		public Expression Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0007E69B File Offset: 0x0007C89B
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x0007E6A3 File Offset: 0x0007C8A3
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x0007E6AC File Offset: 0x0007C8AC
		// (set) Token: 0x06001F87 RID: 8071 RVA: 0x0007E6B4 File Offset: 0x0007C8B4
		[DefaultValue(GroupingDataElementOutputs.Output)]
		public GroupingDataElementOutputs DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0007E6BD File Offset: 0x0007C8BD
		public Grouping()
			: this(null, new GroupExpressions())
		{
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0007E6CC File Offset: 0x0007C8CC
		internal Grouping(string name, ICollection expressions)
			: this(name, null, expressions, false, false, false, null, null)
		{
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0007E6E8 File Offset: 0x0007C8E8
		internal Grouping(Grouping value)
			: this(value.m_name, (value.m_label != null) ? value.m_label.String : null, new GroupExpressions(), value.m_pageBreakAtStart, value.m_pageBreakAtEnd, value.m_naturalGroup, value.m_filters, (value.m_parent != null) ? value.m_parent.String : null)
		{
			this.GroupExpressions.AddRange(value.m_groupExpressions);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0007E75C File Offset: 0x0007C95C
		internal Grouping(string name, string label, ICollection expressions, bool breakAtStart, bool breakAtEnd, bool naturalGroup, Filters filters, string parent)
			: this(name, label, new GroupExpressions(), breakAtStart, breakAtEnd, naturalGroup, filters, parent, null, null, GroupingDataElementOutputs.Output)
		{
			foreach (object obj in expressions)
			{
				string text = (string)obj;
				this.GroupExpressions.Add(new Expression(text));
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0007E7D8 File Offset: 0x0007C9D8
		internal Grouping(string name, string label, GroupExpressions expressions, bool breakAtStart, bool breakAtEnd, bool naturalGroup, Filters filters, string parent, string dataElementName, string dataCollectionName, GroupingDataElementOutputs dataElementOutput)
		{
			this.m_name = name;
			if (label != null && label != string.Empty)
			{
				this.m_label = new Expression(label);
			}
			this.m_groupExpressions = expressions;
			this.m_pageBreakAtStart = breakAtStart;
			this.m_pageBreakAtEnd = breakAtEnd;
			this.m_naturalGroup = naturalGroup;
			this.m_filters = filters;
			this.m_parent = (string.IsNullOrEmpty(parent) ? null : new Expression(parent));
			this.m_dataElementName = dataElementName;
			this.m_dataCollectionName = dataCollectionName;
			this.m_dataElementOutput = dataElementOutput;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0007E868 File Offset: 0x0007CA68
		public void SetGroupExpressions(string[] expressions)
		{
			GroupExpressions groupExpressions = new GroupExpressions();
			for (int i = 0; i < expressions.Length; i++)
			{
				groupExpressions.Add(new Expression(expressions[i]));
			}
			this.m_groupExpressions = groupExpressions;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0007E613 File Offset: 0x0007C813
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x04000DBA RID: 3514
		private string m_name;

		// Token: 0x04000DBB RID: 3515
		private Expression m_label;

		// Token: 0x04000DBC RID: 3516
		private GroupExpressions m_groupExpressions;

		// Token: 0x04000DBD RID: 3517
		private bool m_pageBreakAtStart;

		// Token: 0x04000DBE RID: 3518
		private bool m_pageBreakAtEnd;

		// Token: 0x04000DBF RID: 3519
		private bool m_naturalGroup;

		// Token: 0x04000DC0 RID: 3520
		private Filters m_filters;

		// Token: 0x04000DC1 RID: 3521
		private Expression m_parent;

		// Token: 0x04000DC2 RID: 3522
		private string m_dataElementName;

		// Token: 0x04000DC3 RID: 3523
		private string m_dataCollectionName;

		// Token: 0x04000DC4 RID: 3524
		private GroupingDataElementOutputs m_dataElementOutput;

		// Token: 0x04000DC5 RID: 3525
		public CustomPropertyCollection CustomProperties;
	}
}
