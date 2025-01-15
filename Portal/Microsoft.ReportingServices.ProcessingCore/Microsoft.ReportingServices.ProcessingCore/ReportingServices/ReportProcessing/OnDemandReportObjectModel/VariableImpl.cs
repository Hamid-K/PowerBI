using System;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007C1 RID: 1985
	internal sealed class VariableImpl : Variable
	{
		// Token: 0x06007068 RID: 28776 RVA: 0x001D46B4 File Offset: 0x001D28B4
		internal VariableImpl(Variable variable, IndexedExprHost variableValuesHost, ObjectType parentObjectType, string parentObjectName, ReportRuntime reportRT, int indexInCollection)
		{
			Global.Tracer.Assert(reportRT != null && variable != null, "(null != reportRT && null != variable)");
			this.m_variableDef = variable;
			this.m_exprHost = variableValuesHost;
			this.m_parentObjectType = parentObjectType;
			this.m_parentObjectName = parentObjectName;
			this.m_reportRT = reportRT;
			this.m_indexInCollection = indexInCollection;
		}

		// Token: 0x1700264F RID: 9807
		// (get) Token: 0x06007069 RID: 28777 RVA: 0x001D4710 File Offset: 0x001D2910
		// (set) Token: 0x0600706A RID: 28778 RVA: 0x001D4763 File Offset: 0x001D2963
		public override object Value
		{
			get
			{
				if (!this.m_isValueReady)
				{
					if (this.m_reportRT.ReportObjectModel.OdpContext.IsTablixProcessingMode || this.m_reportRT.VariableReferenceMode)
					{
						return this.GetResult(true);
					}
					return null;
				}
				else
				{
					if (!this.VariableInScope)
					{
						return null;
					}
					return this.m_value;
				}
			}
			set
			{
				this.SetValue(value, false);
			}
		}

		// Token: 0x17002650 RID: 9808
		// (get) Token: 0x0600706B RID: 28779 RVA: 0x001D476D File Offset: 0x001D296D
		public override bool Writable
		{
			get
			{
				return this.m_variableDef.Writable;
			}
		}

		// Token: 0x17002651 RID: 9809
		// (set) Token: 0x0600706C RID: 28780 RVA: 0x001D477A File Offset: 0x001D297A
		internal IScope Scope
		{
			set
			{
				this.m_scope = value;
			}
		}

		// Token: 0x17002652 RID: 9810
		// (get) Token: 0x0600706D RID: 28781 RVA: 0x001D4783 File Offset: 0x001D2983
		// (set) Token: 0x0600706E RID: 28782 RVA: 0x001D4790 File Offset: 0x001D2990
		internal string Name
		{
			get
			{
				return this.m_variableDef.Name;
			}
			set
			{
				this.m_variableDef.Name = value;
			}
		}

		// Token: 0x17002653 RID: 9811
		// (get) Token: 0x0600706F RID: 28783 RVA: 0x001D47A0 File Offset: 0x001D29A0
		private bool VariableInScope
		{
			get
			{
				if (!this.IsReportVariable)
				{
					IRIFReportScope irifreportScope = null;
					if (this.m_reportRT.ReportObjectModel.OdpContext.IsTablixProcessingMode || this.m_reportRT.VariableReferenceMode)
					{
						if (this.m_reportRT.CurrentScope != null)
						{
							irifreportScope = this.m_reportRT.CurrentScope.RIFReportScope;
						}
					}
					else
					{
						IReportScope currentReportScope = this.m_reportRT.ReportObjectModel.OdpContext.CurrentReportScope;
						if (currentReportScope != null)
						{
							irifreportScope = currentReportScope.RIFReportScope;
						}
					}
					if (irifreportScope == null || !irifreportScope.VariableInScope(this.m_variableDef.SequenceID))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17002654 RID: 9812
		// (get) Token: 0x06007070 RID: 28784 RVA: 0x001D4834 File Offset: 0x001D2A34
		private bool IsReportVariable
		{
			get
			{
				return this.m_parentObjectType == ObjectType.Report;
			}
		}

		// Token: 0x06007071 RID: 28785 RVA: 0x001D483F File Offset: 0x001D2A3F
		internal void SetResult(VariantResult result)
		{
			this.m_result = result;
			this.m_isValueReady = true;
		}

		// Token: 0x06007072 RID: 28786 RVA: 0x001D4850 File Offset: 0x001D2A50
		public override bool SetValue(object value)
		{
			bool flag;
			this.SetValue(value, false, out flag);
			return flag;
		}

		// Token: 0x06007073 RID: 28787 RVA: 0x001D4868 File Offset: 0x001D2A68
		internal void SetValue(object value, bool internalSet)
		{
			bool flag;
			this.SetValue(value, internalSet, out flag);
		}

		// Token: 0x06007074 RID: 28788 RVA: 0x001D4880 File Offset: 0x001D2A80
		private void SetValue(object value, bool internalSet, out bool succeeded)
		{
			succeeded = false;
			if (!internalSet)
			{
				if (this.IsReportVariable && this.m_variableDef.Writable)
				{
					this.m_result = new VariantResult(false, value);
					bool flag = this.m_reportRT.ProcessSerializableResult(true, ref this.m_result);
					if (!this.m_result.ErrorOccurred)
					{
						this.m_reportRT.ReportObjectModel.OdpContext.StoreUpdatedVariableValue(this.m_indexInCollection, value);
						succeeded = true;
						this.m_value = value;
						this.m_isValueReady = true;
						return;
					}
					if (flag)
					{
						((IErrorContext)this.m_reportRT).Register(ProcessingErrorCode.rsVariableTypeNotSerializable, Severity.Error, this.m_parentObjectType, this.m_parentObjectName, this.m_variableDef.GetPropertyName(), Array.Empty<string>());
						return;
					}
				}
			}
			else
			{
				succeeded = true;
				this.m_value = value;
				this.m_isValueReady = true;
			}
		}

		// Token: 0x06007075 RID: 28789 RVA: 0x001D494F File Offset: 0x001D2B4F
		internal void Reset()
		{
			this.m_isValueReady = false;
		}

		// Token: 0x06007076 RID: 28790 RVA: 0x001D4958 File Offset: 0x001D2B58
		internal object GetResult()
		{
			return this.GetResult(false);
		}

		// Token: 0x06007077 RID: 28791 RVA: 0x001D4964 File Offset: 0x001D2B64
		private object GetResult(bool fromValue)
		{
			if (fromValue && !this.VariableInScope)
			{
				return null;
			}
			if (!this.m_isValueReady)
			{
				if (this.m_isVisited)
				{
					ProcessingErrorCode processingErrorCode = (this.IsReportVariable ? ProcessingErrorCode.rsCyclicExpressionInReportVariable : ProcessingErrorCode.rsCyclicExpressionInGroupVariable);
					((IErrorContext)this.m_reportRT).Register(processingErrorCode, Severity.Error, this.m_parentObjectType, this.m_parentObjectName, this.m_variableDef.GetPropertyName(), Array.Empty<string>());
					throw new ReportProcessingException(this.m_reportRT.RuntimeErrorContext.Messages);
				}
				this.m_isVisited = true;
				bool variableReferenceMode = this.m_reportRT.VariableReferenceMode;
				ObjectType objectType = this.m_reportRT.ObjectType;
				string objectName = this.m_reportRT.ObjectName;
				string propertyName = this.m_reportRT.PropertyName;
				bool unfulfilledDependency = this.m_reportRT.UnfulfilledDependency;
				IScope currentScope = this.m_reportRT.CurrentScope;
				this.m_reportRT.VariableReferenceMode = true;
				this.m_reportRT.UnfulfilledDependency = false;
				this.m_result = this.m_reportRT.EvaluateVariableValueExpression(this.m_variableDef, this.m_exprHost, this.m_parentObjectType, this.m_parentObjectName, this.IsReportVariable);
				bool unfulfilledDependency2 = this.m_reportRT.UnfulfilledDependency;
				this.m_reportRT.UnfulfilledDependency = unfulfilledDependency;
				this.m_reportRT.VariableReferenceMode = variableReferenceMode;
				this.m_reportRT.CurrentScope = currentScope;
				this.m_reportRT.ObjectType = objectType;
				this.m_reportRT.ObjectName = objectName;
				this.m_reportRT.PropertyName = propertyName;
				if (this.m_result.ErrorOccurred)
				{
					throw new ReportProcessingException(this.m_reportRT.RuntimeErrorContext.Messages);
				}
				if (unfulfilledDependency2 && fromValue)
				{
					this.m_value = null;
					this.m_isValueReady = false;
				}
				else
				{
					this.m_value = this.m_result.Value;
					this.m_isValueReady = true;
				}
				this.m_isVisited = false;
			}
			return this.m_value;
		}

		// Token: 0x04003A0E RID: 14862
		private Variable m_variableDef;

		// Token: 0x04003A0F RID: 14863
		private IndexedExprHost m_exprHost;

		// Token: 0x04003A10 RID: 14864
		private ObjectType m_parentObjectType;

		// Token: 0x04003A11 RID: 14865
		private string m_parentObjectName;

		// Token: 0x04003A12 RID: 14866
		private ReportRuntime m_reportRT;

		// Token: 0x04003A13 RID: 14867
		private IScope m_scope;

		// Token: 0x04003A14 RID: 14868
		private object m_value;

		// Token: 0x04003A15 RID: 14869
		private VariantResult m_result;

		// Token: 0x04003A16 RID: 14870
		private bool m_isValueReady;

		// Token: 0x04003A17 RID: 14871
		private bool m_isVisited;

		// Token: 0x04003A18 RID: 14872
		private int m_indexInCollection;
	}
}
