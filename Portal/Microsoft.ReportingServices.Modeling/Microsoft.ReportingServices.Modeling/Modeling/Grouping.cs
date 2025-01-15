using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B2 RID: 178
	public sealed class Grouping : ModelingObject, ICloneable, IOwned<Hierarchy>, IXmlLoadable, IXmlWriteable, ICompileable, IValidationScope
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x000215EF File Offset: 0x0001F7EF
		public Grouping()
		{
			this.m_details = new ExpressionCollection();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002160D File Offset: 0x0001F80D
		public Grouping(Expression expression)
			: this()
		{
			this.m_expression = expression;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002161C File Offset: 0x0001F81C
		private Grouping(Grouping groupingToCopy)
		{
			this.m_expression = ((groupingToCopy.Expression != null) ? groupingToCopy.Expression.Clone() : null);
			this.m_details = groupingToCopy.Details.Clone();
			this.m_name = groupingToCopy.Name;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00021673 File Offset: 0x0001F873
		internal static Grouping CreateInvalidRefTarget(string name)
		{
			return new Grouping
			{
				Name = name,
				m_invalidRefTarget = true
			};
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00021688 File Offset: 0x0001F888
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00021690 File Offset: 0x0001F890
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				base.CheckWriteable();
				this.m_name = value ?? string.Empty;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000216A8 File Offset: 0x0001F8A8
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x000216B0 File Offset: 0x0001F8B0
		public Expression Expression
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_expression;
			}
			set
			{
				base.CheckWriteable();
				this.m_expression = value;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000216BF File Offset: 0x0001F8BF
		public bool IsEntityGrouping
		{
			get
			{
				return this.m_expression != null && this.m_expression.Node is EntityRefNode;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x000216DE File Offset: 0x0001F8DE
		public ExpressionCollection Details
		{
			get
			{
				return this.m_details;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x000216E6 File Offset: 0x0001F8E6
		public Hierarchy Hierarchy
		{
			get
			{
				return this.m_hierarchy;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x000216EE File Offset: 0x0001F8EE
		public bool IsInvalidRefTarget
		{
			get
			{
				return this.m_invalidRefTarget || this.m_hierarchy == null;
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00021703 File Offset: 0x0001F903
		public Grouping Clone()
		{
			return new Grouping(this);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002170B File Offset: 0x0001F90B
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00021713 File Offset: 0x0001F913
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		void IOwned<Hierarchy>.SetOwner(Hierarchy newHierarchy)
		{
			if (this.m_hierarchy != null && newHierarchy != null)
			{
				throw new InvalidOperationException(DevExceptionMessages.ExistingOwner);
			}
			this.m_hierarchy = newHierarchy;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00021734 File Offset: 0x0001F934
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.Validation.PushScope(this);
			try
			{
				xr.LoadObject("Grouping", this);
			}
			finally
			{
				xr.Validation.PopScope();
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00021780 File Offset: 0x0001F980
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "Name")
			{
				this.m_name = xr.ReadValueAsString();
				return true;
			}
			return false;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000217AC File Offset: 0x0001F9AC
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "Expression")
				{
					this.m_expression = new Expression();
					this.m_expression.Load(xr, true);
					return true;
				}
				if (localName == "Details")
				{
					this.m_details.Load(xr, true);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00021810 File Offset: 0x0001FA10
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Grouping");
			xw.WriteAttribute("Name", this.m_name);
			if (this.m_expression != null)
			{
				this.m_expression.WriteTo(xw);
			}
			this.m_details.WriteTo(xw, "Details");
			xw.WriteEndElement();
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00021864 File Offset: 0x0001FA64
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00021870 File Offset: 0x0001FA70
		internal void Compile(CompilationContext ctx)
		{
			if (this.m_name.Length == 0)
			{
				ctx.AddScopedError(ModelingErrorCode.MissingGroupingName, SRErrors.MissingGroupingName(ctx.CurrentObjectDescriptor));
			}
			ctx.PushScope(this);
			try
			{
				base.Compile(ctx.ShouldPersist);
				if (this.m_expression == null)
				{
					ctx.AddScopedError(ModelingErrorCode.MissingGroupingExpression, SRErrors.MissingGroupingExpression(ctx.CurrentObjectDescriptor));
				}
				else
				{
					ResultType? resultType = this.m_expression.Compile(ctx, ExpressionCompilationFlags.GroupingExpression);
					if (resultType != null)
					{
						if (resultType.Value.DataType == DataType.Binary)
						{
							ctx.AddScopedError(ModelingErrorCode.BinaryGroupingExpression, SRErrors.BinaryGroupingExpression(SRObjectDescriptor.FromScope(this.m_expression)));
						}
						if (this.IsEntityGrouping)
						{
							ctx.PushContextEntity(this.m_expression.NodeAsEntityRef.Entity);
							try
							{
								this.m_details.Compile(ctx, ExpressionCompilationFlags.GroupingDetail);
								return;
							}
							finally
							{
								ctx.PopContextEntity();
							}
						}
						if (this.m_details.Count == 0)
						{
							this.m_details.Compile(ctx, ExpressionCompilationFlags.GroupingDetail);
						}
						else
						{
							ctx.AddScopedError(ModelingErrorCode.NonEntityGroupingWithDetails, SRErrors.NonEntityGroupingWithDetails(ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.m_expression)));
						}
					}
				}
			}
			finally
			{
				ctx.PopScope();
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000219B8 File Offset: 0x0001FBB8
		void ICompileable.Compile(CompilationContext ctx)
		{
			this.Compile(ctx);
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x000219C1 File Offset: 0x0001FBC1
		string IValidationScope.ObjectType
		{
			get
			{
				return "Grouping";
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000219C8 File Offset: 0x0001FBC8
		string IValidationScope.ObjectID
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x000219D0 File Offset: 0x0001FBD0
		string IValidationScope.ObjectName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x0400044A RID: 1098
		internal const string GroupingElem = "Grouping";

		// Token: 0x0400044B RID: 1099
		private const string DetailsElem = "Details";

		// Token: 0x0400044C RID: 1100
		private const string NameAttr = "Name";

		// Token: 0x0400044D RID: 1101
		private Expression m_expression;

		// Token: 0x0400044E RID: 1102
		private readonly ExpressionCollection m_details;

		// Token: 0x0400044F RID: 1103
		private string m_name = string.Empty;

		// Token: 0x04000450 RID: 1104
		private bool m_invalidRefTarget;

		// Token: 0x04000451 RID: 1105
		private Hierarchy m_hierarchy;
	}
}
