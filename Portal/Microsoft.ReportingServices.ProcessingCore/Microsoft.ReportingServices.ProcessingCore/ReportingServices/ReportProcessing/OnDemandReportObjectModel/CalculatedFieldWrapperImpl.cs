using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007B0 RID: 1968
	internal sealed class CalculatedFieldWrapperImpl : CalculatedFieldWrapper, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06006FCC RID: 28620 RVA: 0x001D21A2 File Offset: 0x001D03A2
		internal CalculatedFieldWrapperImpl()
		{
		}

		// Token: 0x06006FCD RID: 28621 RVA: 0x001D21AA File Offset: 0x001D03AA
		internal CalculatedFieldWrapperImpl(Microsoft.ReportingServices.ReportIntermediateFormat.Field fieldDef, ReportRuntime reportRT)
		{
			this.m_fieldDef = fieldDef;
			this.m_reportRT = reportRT;
			this.m_iErrorContext = reportRT;
		}

		// Token: 0x17002616 RID: 9750
		// (get) Token: 0x06006FCE RID: 28622 RVA: 0x001D21C7 File Offset: 0x001D03C7
		public override object Value
		{
			get
			{
				if (!this.m_isValueReady)
				{
					this.CalculateValue();
				}
				return this.m_value;
			}
		}

		// Token: 0x06006FCF RID: 28623 RVA: 0x001D21DD File Offset: 0x001D03DD
		internal void ResetValue()
		{
			this.m_isValueReady = false;
			this.m_isVisited = false;
			this.m_value = null;
		}

		// Token: 0x17002617 RID: 9751
		// (get) Token: 0x06006FD0 RID: 28624 RVA: 0x001D21F4 File Offset: 0x001D03F4
		internal bool ErrorOccurred
		{
			get
			{
				if (!this.m_isValueReady)
				{
					this.CalculateValue();
				}
				return this.m_errorOccurred;
			}
		}

		// Token: 0x17002618 RID: 9752
		// (get) Token: 0x06006FD1 RID: 28625 RVA: 0x001D220A File Offset: 0x001D040A
		internal string ExceptionMessage
		{
			get
			{
				return this.m_exceptionMessage;
			}
		}

		// Token: 0x06006FD2 RID: 28626 RVA: 0x001D2214 File Offset: 0x001D0414
		private void CalculateValue()
		{
			if (this.m_isVisited)
			{
				this.m_iErrorContext.Register(ProcessingErrorCode.rsCyclicExpression, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.Field, this.m_fieldDef.Name, "Value", Array.Empty<string>());
				throw new ReportProcessingException_InvalidOperationException();
			}
			this.m_isVisited = true;
			VariantResult variantResult = this.m_reportRT.EvaluateFieldValueExpression(this.m_fieldDef);
			this.m_value = variantResult.Value;
			this.m_errorOccurred = variantResult.ErrorOccurred;
			if (this.m_errorOccurred)
			{
				this.m_exceptionMessage = variantResult.ExceptionMessage;
			}
			this.m_isVisited = false;
			this.m_isValueReady = true;
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x001D22AC File Offset: 0x001D04AC
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(CalculatedFieldWrapperImpl.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ReportRuntime)
				{
					if (memberName == MemberName.Value)
					{
						writer.Write(this.m_value);
						continue;
					}
					if (memberName == MemberName.FieldDef)
					{
						int num = scalabilityCache.StoreStaticReference(this.m_fieldDef);
						writer.Write(num);
						continue;
					}
					if (memberName == MemberName.ReportRuntime)
					{
						int num2 = scalabilityCache.StoreStaticReference(this.m_reportRT);
						writer.Write(num2);
						continue;
					}
				}
				else if (memberName <= MemberName.IsValueReady)
				{
					if (memberName == MemberName.ErrorOccurred)
					{
						writer.Write(this.m_errorOccurred);
						continue;
					}
					if (memberName == MemberName.IsValueReady)
					{
						writer.Write(this.m_isValueReady);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.IsVisited)
					{
						writer.Write(this.m_isVisited);
						continue;
					}
					if (memberName == MemberName.ExceptionMessage)
					{
						writer.Write(this.m_exceptionMessage);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x001D23BC File Offset: 0x001D05BC
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(CalculatedFieldWrapperImpl.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ReportRuntime)
				{
					if (memberName == MemberName.Value)
					{
						this.m_value = reader.ReadVariant();
						continue;
					}
					if (memberName == MemberName.FieldDef)
					{
						int num = reader.ReadInt32();
						this.m_fieldDef = (Microsoft.ReportingServices.ReportIntermediateFormat.Field)scalabilityCache.FetchStaticReference(num);
						continue;
					}
					if (memberName == MemberName.ReportRuntime)
					{
						int num2 = reader.ReadInt32();
						this.m_reportRT = (ReportRuntime)scalabilityCache.FetchStaticReference(num2);
						this.m_iErrorContext = this.m_reportRT;
						continue;
					}
				}
				else if (memberName <= MemberName.IsValueReady)
				{
					if (memberName == MemberName.ErrorOccurred)
					{
						this.m_errorOccurred = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.IsValueReady)
					{
						this.m_isValueReady = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.IsVisited)
					{
						this.m_isVisited = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.ExceptionMessage)
					{
						this.m_exceptionMessage = reader.ReadString();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06006FD5 RID: 28629 RVA: 0x001D24EE File Offset: 0x001D06EE
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06006FD6 RID: 28630 RVA: 0x001D24F0 File Offset: 0x001D06F0
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CalculatedFieldWrapperImpl;
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x001D24F4 File Offset: 0x001D06F4
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (CalculatedFieldWrapperImpl.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CalculatedFieldWrapperImpl, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.FieldDef, Token.Int32),
					new MemberInfo(MemberName.Value, Token.Object),
					new MemberInfo(MemberName.IsValueReady, Token.Boolean),
					new MemberInfo(MemberName.IsVisited, Token.Boolean),
					new MemberInfo(MemberName.ReportRuntime, Token.Int32),
					new MemberInfo(MemberName.ErrorOccurred, Token.Boolean),
					new MemberInfo(MemberName.ExceptionMessage, Token.String)
				});
			}
			return CalculatedFieldWrapperImpl.m_declaration;
		}

		// Token: 0x17002619 RID: 9753
		// (get) Token: 0x06006FD8 RID: 28632 RVA: 0x001D25A6 File Offset: 0x001D07A6
		public int Size
		{
			get
			{
				return ItemSizes.ReferenceSize + ItemSizes.SizeOf(this.m_value) + 1 + 1 + ItemSizes.ReferenceSize + ItemSizes.ReferenceSize + 1 + ItemSizes.SizeOf(this.m_exceptionMessage);
			}
		}

		// Token: 0x040039C2 RID: 14786
		private Microsoft.ReportingServices.ReportIntermediateFormat.Field m_fieldDef;

		// Token: 0x040039C3 RID: 14787
		private object m_value;

		// Token: 0x040039C4 RID: 14788
		private bool m_isValueReady;

		// Token: 0x040039C5 RID: 14789
		private bool m_isVisited;

		// Token: 0x040039C6 RID: 14790
		private ReportRuntime m_reportRT;

		// Token: 0x040039C7 RID: 14791
		private bool m_errorOccurred;

		// Token: 0x040039C8 RID: 14792
		private string m_exceptionMessage;

		// Token: 0x040039C9 RID: 14793
		[NonSerialized]
		private IErrorContext m_iErrorContext;

		// Token: 0x040039CA RID: 14794
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = CalculatedFieldWrapperImpl.GetDeclaration();
	}
}
