using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000B6 RID: 182
	public sealed class Parameter : ModelingObject, ICloneable, IXmlLoadable, IXmlWriteable, ICompileable, IValidationScope
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0002268C File Offset: 0x0002088C
		public Parameter()
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002269F File Offset: 0x0002089F
		public Parameter(string name, DataType dataType)
		{
			this.Name = name;
			this.DataType = dataType;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000226C0 File Offset: 0x000208C0
		private Parameter(Parameter paramToCopy, ExpressionCopyManager copyManager)
		{
			this.m_name = paramToCopy.Name;
			this.m_dataType = paramToCopy.DataType;
			this.m_nullable = paramToCopy.Nullable;
			this.m_cardinality = paramToCopy.Cardinality;
			if (paramToCopy.DefaultValue != null)
			{
				this.m_defaultValueExpr = paramToCopy.DefaultValue.Clone(copyManager);
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00022728 File Offset: 0x00020928
		internal static Parameter CreateInvalidRefTarget(string name)
		{
			return new Parameter
			{
				Name = name,
				m_invalidRefTarget = true
			};
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002273D File Offset: 0x0002093D
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00022745 File Offset: 0x00020945
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

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002275D File Offset: 0x0002095D
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00022765 File Offset: 0x00020965
		public DataType DataType
		{
			get
			{
				return this.m_dataType;
			}
			set
			{
				if (!EnumUtil.IsDefined<DataType>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				if (value == DataType.Binary || value == DataType.Null)
				{
					throw new ArgumentException(DevExceptionMessages.Parameter_DataTypeBinaryOrNull);
				}
				base.CheckWriteable();
				this.m_dataType = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00022795 File Offset: 0x00020995
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0002279D File Offset: 0x0002099D
		public bool Nullable
		{
			get
			{
				return this.m_nullable;
			}
			set
			{
				base.CheckWriteable();
				this.m_nullable = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x000227AC File Offset: 0x000209AC
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x000227B4 File Offset: 0x000209B4
		public Cardinality Cardinality
		{
			get
			{
				return this.m_cardinality;
			}
			set
			{
				if (!EnumUtil.IsDefined<Cardinality>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_cardinality = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000227D1 File Offset: 0x000209D1
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x000227D9 File Offset: 0x000209D9
		public Expression DefaultValue
		{
			get
			{
				return this.m_defaultValueExpr;
			}
			set
			{
				base.CheckWriteable();
				this.m_defaultValueExpr = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x000227E8 File Offset: 0x000209E8
		public bool IsOptional
		{
			get
			{
				return this.m_defaultValueExpr != null || this.m_name == "DrillthroughSourceQuery" || this.m_name == "DrillthroughContext";
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00022816 File Offset: 0x00020A16
		public bool IsInvalidRefTarget
		{
			get
			{
				return this.m_invalidRefTarget;
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002281E File Offset: 0x00020A1E
		public Parameter Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00022827 File Offset: 0x00020A27
		public Parameter Clone(ExpressionCopyManager copyManager)
		{
			return new Parameter(this, copyManager);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00022830 File Offset: 0x00020A30
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00022838 File Offset: 0x00020A38
		internal void Load(ModelingXmlReader xr)
		{
			base.CheckWriteable();
			xr.Validation.PushScope(this);
			try
			{
				xr.LoadObject("Parameter", this);
			}
			finally
			{
				xr.Validation.PopScope();
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00022884 File Offset: 0x00020A84
		bool IXmlLoadable.LoadXmlAttribute(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace && xr.LocalName == "Name")
			{
				this.m_name = xr.ReadValueAsString();
				return true;
			}
			return false;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000228B0 File Offset: 0x00020AB0
		bool IXmlLoadable.LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName == "DataType")
				{
					this.m_dataType = xr.ReadValueAsEnum<DataType>();
					return true;
				}
				if (localName == "Nullable")
				{
					this.m_nullable = xr.ReadValueAsBoolean();
					return true;
				}
				if (localName == "Cardinality")
				{
					this.m_cardinality = xr.ReadValueAsEnum<Cardinality>();
					return true;
				}
				if (localName == "Expression")
				{
					this.m_defaultValueExpr = new Expression();
					this.m_defaultValueExpr.Load(xr, false);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002294C File Offset: 0x00020B4C
		internal void WriteTo(ModelingXmlWriter xw)
		{
			xw.WriteStartElement("Parameter");
			xw.WriteAttribute("Name", this.m_name);
			xw.WriteElement("DataType", this.m_dataType);
			xw.WriteElementIfNonDefault<bool>("Nullable", this.m_nullable);
			xw.WriteElementIfNonDefault<Cardinality>("Cardinality", this.m_cardinality);
			if (this.m_defaultValueExpr != null)
			{
				this.m_defaultValueExpr.WriteTo(xw);
			}
			xw.WriteEndElement();
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000229C7 File Offset: 0x00020BC7
		void IXmlWriteable.WriteTo(ModelingXmlWriter xw)
		{
			this.WriteTo(xw);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x000229D0 File Offset: 0x00020BD0
		internal void Compile(CompilationContext ctx)
		{
			base.Compile(ctx.ShouldPersist);
			if (this.m_name.Length == 0)
			{
				ctx.AddScopedError(ModelingErrorCode.MissingParameterName, SRErrors.MissingParameterName(ctx.CurrentObjectDescriptor));
			}
			if (this.m_name == "DrillthroughSourceQuery" || this.m_name == "DrillthroughContext")
			{
				ctx.AddScopedError(ModelingErrorCode.InvalidParameterName, SRErrors.InvalidParameterName(ctx.CurrentObjectDescriptor, this.m_name, this.m_name));
			}
			ctx.PushScope(this);
			try
			{
				this.m_compiledExprIsValid = false;
				if (this.m_defaultValueExpr != null)
				{
					ResultType? resultType = this.m_defaultValueExpr.Compile(ctx, ExpressionCompilationFlags.None);
					if (resultType != null)
					{
						bool flag = true;
						ResultType value = resultType.Value;
						if (!this.CheckDefaultValue(this.m_defaultValueExpr))
						{
							ctx.AddScopedError(ModelingErrorCode.InvalidParameterExpression, SRErrors.InvalidParameterExpression(ctx.CurrentObjectDescriptor));
							flag = false;
						}
						if (value.DataType != this.m_dataType && value.DataType != DataType.Null)
						{
							ctx.AddScopedError(ModelingErrorCode.ParameterExpressionDataTypeMismatch, SRErrors.ParameterExpressionDataTypeMismatch(ctx.CurrentObjectDescriptor, ctx.CurrentObjectDescriptor.ObjectName, this.m_dataType, value.DataType));
							flag = false;
						}
						if (value.Nullable && !this.m_nullable)
						{
							ctx.AddScopedError(ModelingErrorCode.ParameterExpressionNullableMismatch, SRErrors.ParameterExpressionNullableMismatch(ctx.CurrentObjectDescriptor, ctx.CurrentObjectDescriptor.ObjectName));
							flag = false;
						}
						if (value.Cardinality == Cardinality.Many && this.m_cardinality == Cardinality.One)
						{
							ctx.AddScopedError(ModelingErrorCode.ParameterExpressionCardinalityMismatch, SRErrors.ParameterExpressionCardinalityMismatch(ctx.CurrentObjectDescriptor, ctx.CurrentObjectDescriptor.ObjectName));
							flag = false;
						}
						this.m_compiledExprIsValid = flag;
					}
				}
			}
			finally
			{
				ctx.PopScope();
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00022B90 File Offset: 0x00020D90
		private bool CheckDefaultValue(Expression expr)
		{
			return expr.Path.IsEmpty && expr.Node.IsConstantValue && expr.NodeAsParameterRef == null;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00022BB8 File Offset: 0x00020DB8
		internal Expression CreateReplacementExpression(CompilationContext ctx)
		{
			if (ctx.ParameterValues == null)
			{
				throw new InternalModelingException("ctx.ParameterValues is null");
			}
			if (!base.IsCompiled)
			{
				throw new InternalModelingException("Parameter is not compiled");
			}
			if (!ctx.ParameterValues.ContainsKey(this.m_name))
			{
				if (!this.m_compiledExprIsValid)
				{
					return null;
				}
				if (!this.CheckDefaultValue(this.m_defaultValueExpr))
				{
					throw new InternalModelingException(StringUtil.FormatInvariant("m_defaultValueExpr is invalid '{0}'", new object[] { this.m_defaultValueExpr.ToString() }));
				}
				if (this.m_defaultValueExpr.NodeAsLiteral == null)
				{
					return this.m_defaultValueExpr.Clone();
				}
				if (this.m_cardinality != Cardinality.One)
				{
					return new Expression(this.m_defaultValueExpr.NodeAsLiteral.ToSet());
				}
				if (this.m_defaultValueExpr.NodeAsLiteral.Cardinality != Cardinality.One)
				{
					throw new InternalModelingException("m_defaultValueExpr cardinality mismatch for compiled parameter");
				}
				return this.m_defaultValueExpr.Clone();
			}
			else
			{
				object obj = ctx.ParameterValues[this.m_name];
				if (obj == null || obj == DBNull.Value)
				{
					if (!this.m_nullable)
					{
						ctx.AddScopedError(ModelingErrorCode.NullParameterValue, SRErrors.NullParameterValue(this.m_name));
						return null;
					}
					return new Expression(new NullNode());
				}
				else
				{
					if (!(obj is IList))
					{
						LiteralNode literalNode;
						if (this.m_cardinality == Cardinality.One)
						{
							literalNode = LiteralNode.FromObject(obj, this.m_dataType);
						}
						else
						{
							literalNode = LiteralNode.FromObjectList(new object[] { obj }, this.m_dataType);
						}
						if (literalNode == null)
						{
							ctx.AddScopedError(ModelingErrorCode.InvalidParameterValueType, SRErrors.InvalidParameterValueType(this.m_name, this.m_dataType));
						}
						return new Expression(literalNode);
					}
					if (this.m_cardinality != Cardinality.Many)
					{
						ctx.AddScopedError(ModelingErrorCode.InvalidParameterValueCardinality, SRErrors.InvalidParameterValueCardinality(this.m_name));
						return null;
					}
					LiteralNode literalNode2 = LiteralNode.FromObjectList((IList)obj, this.m_dataType);
					if (literalNode2 == null)
					{
						ctx.AddScopedError(ModelingErrorCode.InvalidParameterValueType, SRErrors.InvalidParameterValueType_MultipleValues(this.m_name, this.m_dataType));
					}
					return new Expression(literalNode2);
				}
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00022D8D File Offset: 0x00020F8D
		void ICompileable.Compile(CompilationContext ctx)
		{
			this.Compile(ctx);
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00022D96 File Offset: 0x00020F96
		string IValidationScope.ObjectType
		{
			get
			{
				return "Parameter";
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00022D9D File Offset: 0x00020F9D
		string IValidationScope.ObjectID
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00022DA5 File Offset: 0x00020FA5
		string IValidationScope.ObjectName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00022DAD File Offset: 0x00020FAD
		private static Parameter CreateDrillthroughContextInstance(string name)
		{
			Parameter parameter = new Parameter();
			parameter.Name = name;
			parameter.DataType = DataType.String;
			parameter.Cardinality = Cardinality.One;
			parameter.Compile(true);
			return parameter;
		}

		// Token: 0x04000470 RID: 1136
		public const string DrillthroughSourceQuery = "DrillthroughSourceQuery";

		// Token: 0x04000471 RID: 1137
		public const string DrillthroughContext = "DrillthroughContext";

		// Token: 0x04000472 RID: 1138
		internal const string ParameterElem = "Parameter";

		// Token: 0x04000473 RID: 1139
		private const string NameAttr = "Name";

		// Token: 0x04000474 RID: 1140
		private const string DataTypeElem = "DataType";

		// Token: 0x04000475 RID: 1141
		private const string NullableElem = "Nullable";

		// Token: 0x04000476 RID: 1142
		private const string CardinalityElem = "Cardinality";

		// Token: 0x04000477 RID: 1143
		private string m_name = string.Empty;

		// Token: 0x04000478 RID: 1144
		private DataType m_dataType;

		// Token: 0x04000479 RID: 1145
		private bool m_nullable;

		// Token: 0x0400047A RID: 1146
		private Cardinality m_cardinality;

		// Token: 0x0400047B RID: 1147
		private Expression m_defaultValueExpr;

		// Token: 0x0400047C RID: 1148
		private bool m_invalidRefTarget;

		// Token: 0x0400047D RID: 1149
		private bool m_compiledExprIsValid;

		// Token: 0x0400047E RID: 1150
		internal static readonly Parameter DrillthroughSourceQueryInstance = Parameter.CreateDrillthroughContextInstance("DrillthroughSourceQuery");

		// Token: 0x0400047F RID: 1151
		internal static readonly Parameter DrillthroughContextInstance = Parameter.CreateDrillthroughContextInstance("DrillthroughContext");
	}
}
