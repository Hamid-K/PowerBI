using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C7 RID: 1223
	public sealed class RuntimeExpressionInfo : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003E26 RID: 15910 RVA: 0x0010A0AF File Offset: 0x001082AF
		internal RuntimeExpressionInfo()
		{
		}

		// Token: 0x06003E27 RID: 15911 RVA: 0x0010A0C0 File Offset: 0x001082C0
		internal RuntimeExpressionInfo(List<ExpressionInfo> expressions, IndexedExprHost expressionsHost, List<bool> directions, int expressionIndex)
		{
			this.m_expressionsHost = expressionsHost;
			this.m_expressionIndex = expressionIndex;
			this.m_expression = expressions[this.m_expressionIndex];
			if (directions != null)
			{
				this.m_direction = directions[this.m_expressionIndex];
			}
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x0010A110 File Offset: 0x00108310
		internal ExpressionInfo Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x0010A118 File Offset: 0x00108318
		internal bool Direction
		{
			get
			{
				return this.m_direction;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x0010A120 File Offset: 0x00108320
		internal IndexedExprHost ExpressionsHost
		{
			get
			{
				return this.m_expressionsHost;
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x0010A128 File Offset: 0x00108328
		internal int ExpressionIndex
		{
			get
			{
				return this.m_expressionIndex;
			}
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x0010A130 File Offset: 0x00108330
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeExpressionInfo.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Expression)
				{
					switch (memberName)
					{
					case MemberName.Direction:
						writer.Write(this.m_direction);
						break;
					case MemberName.ExpressionsHost:
					{
						int num = scalabilityCache.StoreStaticReference(this.m_expressionsHost);
						writer.Write(num);
						break;
					}
					case MemberName.ExpressionIndex:
						writer.Write(this.m_expressionIndex);
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					int num2 = scalabilityCache.StoreStaticReference(this.m_expression);
					writer.Write(num2);
				}
			}
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x0010A1F0 File Offset: 0x001083F0
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeExpressionInfo.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Expression)
				{
					switch (memberName)
					{
					case MemberName.Direction:
						this.m_direction = reader.ReadBoolean();
						break;
					case MemberName.ExpressionsHost:
					{
						int num = reader.ReadInt32();
						this.m_expressionsHost = (IndexedExprHost)scalabilityCache.FetchStaticReference(num);
						break;
					}
					case MemberName.ExpressionIndex:
						this.m_expressionIndex = reader.ReadInt32();
						break;
					default:
						Global.Tracer.Assert(false);
						break;
					}
				}
				else
				{
					int num2 = reader.ReadInt32();
					this.m_expression = (ExpressionInfo)scalabilityCache.FetchStaticReference(num2);
				}
			}
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x0010A2B7 File Offset: 0x001084B7
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x0010A2B9 File Offset: 0x001084B9
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo;
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x0010A2C0 File Offset: 0x001084C0
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeExpressionInfo.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Expression, Token.Int32),
					new MemberInfo(MemberName.Direction, Token.Boolean),
					new MemberInfo(MemberName.ExpressionsHost, Token.Int32),
					new MemberInfo(MemberName.ExpressionIndex, Token.Int32)
				});
			}
			return RuntimeExpressionInfo.m_declaration;
		}

		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x06003E31 RID: 15921 RVA: 0x0010A33D File Offset: 0x0010853D
		public int Size
		{
			get
			{
				return ItemSizes.ReferenceSize + 1 + ItemSizes.ReferenceSize + 4;
			}
		}

		// Token: 0x04001CF7 RID: 7415
		[StaticReference]
		private ExpressionInfo m_expression;

		// Token: 0x04001CF8 RID: 7416
		private bool m_direction = true;

		// Token: 0x04001CF9 RID: 7417
		[StaticReference]
		private IndexedExprHost m_expressionsHost;

		// Token: 0x04001CFA RID: 7418
		private int m_expressionIndex;

		// Token: 0x04001CFB RID: 7419
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeExpressionInfo.GetDeclaration();
	}
}
