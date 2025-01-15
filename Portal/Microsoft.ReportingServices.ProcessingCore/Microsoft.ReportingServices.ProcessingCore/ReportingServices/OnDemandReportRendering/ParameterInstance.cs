using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002DF RID: 735
	public sealed class ParameterInstance : BaseInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06001B71 RID: 7025 RVA: 0x0006E089 File Offset: 0x0006C289
		internal ParameterInstance(ActionItemInstance actionInstance, int index)
			: base(null)
		{
			this.m_isOldSnapshot = true;
			this.SetMembers(actionInstance, index);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0006E0A1 File Offset: 0x0006C2A1
		internal ParameterInstance(Parameter parameterDef)
			: base(parameterDef.ActionDef.Owner.ReportScope)
		{
			this.m_isOldSnapshot = false;
			this.m_parameterDef = parameterDef;
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0006E0C8 File Offset: 0x0006C2C8
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x0006E1C0 File Offset: 0x0006C3C0
		public object Value
		{
			get
			{
				if (!this.m_isOldSnapshot && !this.m_valueReady)
				{
					this.m_valueReady = true;
					if (!this.m_parameterDef.Value.IsExpression)
					{
						this.m_value = this.m_parameterDef.Value.Value;
					}
					else if (this.m_parameterDef.ActionDef.Owner.ReportElementOwner == null || this.m_parameterDef.ActionDef.Owner.ReportElementOwner.CriOwner == null)
					{
						ActionInfo owner = this.m_parameterDef.ActionDef.Owner;
						this.m_value = this.m_parameterDef.ActionDef.ActionItemDef.EvaluateDrillthroughParamValue(this.ReportScopeInstance, owner.RenderingContext.OdpContext, owner.InstancePath, owner.ROMActionOwner.FieldsUsedInValueExpression, this.m_parameterDef.ParameterDef, owner.ObjectType, owner.ObjectName);
					}
				}
				return this.m_value;
			}
			set
			{
				ReportElement reportElementOwner = this.m_parameterDef.ActionDef.Owner.ReportElementOwner;
				Global.Tracer.Assert(this.m_parameterDef.Value != null, "(m_parameterDef.Value != null)");
				if (!this.m_parameterDef.ActionDef.Owner.IsChartConstruction && (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_parameterDef.Value.IsExpression)))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				if (value != null)
				{
					if (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition)
					{
						if (!(value is string))
						{
							throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWritebackStringExpected);
						}
					}
					else
					{
						bool flag;
						if (value is object[])
						{
							object[] array = (object[])value;
							flag = true;
							object[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								if (!ReportRuntime.IsVariant(array2[i]))
								{
									flag = false;
									break;
								}
							}
						}
						else
						{
							flag = ReportRuntime.IsVariant(value);
						}
						if (!flag)
						{
							throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
						}
					}
				}
				this.m_valueReady = true;
				this.m_value = value;
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0006E2B8 File Offset: 0x0006C4B8
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0006E3A4 File Offset: 0x0006C5A4
		public bool Omit
		{
			get
			{
				if (!this.m_isOldSnapshot && !this.m_omitReady)
				{
					this.m_omitReady = true;
					if (!this.m_parameterDef.Omit.IsExpression)
					{
						this.m_omit = this.m_parameterDef.Omit.Value;
					}
					else if (this.m_parameterDef.ActionDef.Owner.ReportElementOwner == null || this.m_parameterDef.ActionDef.Owner.ReportElementOwner.CriOwner == null)
					{
						ActionInfo owner = this.m_parameterDef.ActionDef.Owner;
						this.m_omit = this.m_parameterDef.ActionDef.ActionItemDef.EvaluateDrillthroughParamOmit(this.ReportScopeInstance, owner.RenderingContext.OdpContext, owner.InstancePath, this.m_parameterDef.ParameterDef, owner.ObjectType, owner.ObjectName);
					}
				}
				return this.m_omit;
			}
			set
			{
				ReportElement reportElementOwner = this.m_parameterDef.ActionDef.Owner.ReportElementOwner;
				Global.Tracer.Assert(this.m_parameterDef.Omit != null, "(m_parameterDef.Omit != null)");
				if (!this.m_parameterDef.ActionDef.Owner.IsChartConstruction && (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.None || (reportElementOwner.CriGenerationPhase == ReportElement.CriGenerationPhases.Instance && !this.m_parameterDef.Omit.IsExpression)))
				{
					throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
				}
				this.m_omitReady = true;
				this.m_omitAssigned = true;
				this.m_omit = value;
			}
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0006E43E File Offset: 0x0006C63E
		internal bool IsOmitAssined
		{
			get
			{
				return this.m_omitAssigned;
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0006E448 File Offset: 0x0006C648
		private void SetMembers(ActionItemInstance actionInstance, int index)
		{
			this.m_value = null;
			this.m_omit = false;
			if (actionInstance != null)
			{
				if (actionInstance.DrillthroughParametersValues != null)
				{
					this.m_value = actionInstance.DrillthroughParametersValues[index];
				}
				if (actionInstance.DrillthroughParametersOmits != null)
				{
					this.m_omit = actionInstance.DrillthroughParametersOmits[index];
				}
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0006E496 File Offset: 0x0006C696
		internal void Update(ActionItemInstance actionInstance, int index)
		{
			this.SetMembers(actionInstance, index);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0006E4A0 File Offset: 0x0006C6A0
		internal override void SetNewContext()
		{
			base.SetNewContext();
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0006E4A8 File Offset: 0x0006C6A8
		protected override void ResetInstanceCache()
		{
			this.m_omitAssigned = false;
			this.m_omitReady = false;
			this.m_valueReady = false;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0006E4C0 File Offset: 0x0006C6C0
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ParameterInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.Omit)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						object obj = null;
						if (this.m_parameterDef.Omit.IsExpression && this.IsOmitAssined)
						{
							obj = this.Omit;
						}
						writer.Write(obj);
					}
				}
				else
				{
					object obj2 = null;
					if (this.m_parameterDef.Value.IsExpression)
					{
						obj2 = this.Value;
					}
					writer.Write(obj2);
				}
			}
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0006E568 File Offset: 0x0006C768
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ParameterInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Value)
				{
					if (memberName != MemberName.Omit)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						object obj = reader.ReadVariant();
						if (this.m_parameterDef.Omit.IsExpression && obj != null)
						{
							this.m_omitReady = true;
							this.m_omit = (bool)obj;
						}
						else
						{
							Global.Tracer.Assert(obj == null, "(omit == null)");
						}
					}
				}
				else
				{
					object obj2 = reader.ReadVariant();
					if (this.m_parameterDef.Value.IsExpression)
					{
						this.m_valueReady = true;
						this.m_value = obj2;
					}
					else
					{
						Global.Tracer.Assert(obj2 == null, "(value == null)");
					}
				}
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0006E642 File Offset: 0x0006C842
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0006E64F File Offset: 0x0006C84F
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInstance;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0006E658 File Offset: 0x0006C858
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Value, Token.Object),
				new MemberInfo(MemberName.Omit, Token.Object)
			});
		}

		// Token: 0x04000D93 RID: 3475
		private bool m_omit;

		// Token: 0x04000D94 RID: 3476
		private object m_value;

		// Token: 0x04000D95 RID: 3477
		[NonSerialized]
		private bool m_isOldSnapshot;

		// Token: 0x04000D96 RID: 3478
		[NonSerialized]
		private bool m_valueReady;

		// Token: 0x04000D97 RID: 3479
		[NonSerialized]
		private bool m_omitReady;

		// Token: 0x04000D98 RID: 3480
		[NonSerialized]
		private bool m_omitAssigned;

		// Token: 0x04000D99 RID: 3481
		[NonSerialized]
		private Parameter m_parameterDef;

		// Token: 0x04000D9A RID: 3482
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterInstance.GetDeclaration();
	}
}
