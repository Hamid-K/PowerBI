using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000507 RID: 1287
	[Serializable]
	public sealed class Sorting : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060043B8 RID: 17336 RVA: 0x0011C1B6 File Offset: 0x0011A3B6
		internal Sorting(ConstructionPhase phase)
		{
			if (phase == ConstructionPhase.Publishing)
			{
				this.m_sortExpressions = new List<ExpressionInfo>();
				this.m_sortDirections = new List<bool>();
				this.m_naturalSortFlags = new List<bool>();
				this.m_deferredSortFlags = new List<bool>();
			}
		}

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x0011C1ED File Offset: 0x0011A3ED
		// (set) Token: 0x060043BA RID: 17338 RVA: 0x0011C1F5 File Offset: 0x0011A3F5
		internal List<ExpressionInfo> SortExpressions
		{
			get
			{
				return this.m_sortExpressions;
			}
			set
			{
				this.m_sortExpressions = value;
			}
		}

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x0011C1FE File Offset: 0x0011A3FE
		// (set) Token: 0x060043BC RID: 17340 RVA: 0x0011C206 File Offset: 0x0011A406
		internal List<bool> SortDirections
		{
			get
			{
				return this.m_sortDirections;
			}
			set
			{
				this.m_sortDirections = value;
			}
		}

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x0011C20F File Offset: 0x0011A40F
		// (set) Token: 0x060043BE RID: 17342 RVA: 0x0011C217 File Offset: 0x0011A417
		internal List<bool> NaturalSortFlags
		{
			get
			{
				return this.m_naturalSortFlags;
			}
			set
			{
				this.m_naturalSortFlags = value;
			}
		}

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x0011C220 File Offset: 0x0011A420
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x0011C228 File Offset: 0x0011A428
		internal List<bool> DeferredSortFlags
		{
			get
			{
				return this.m_deferredSortFlags;
			}
			set
			{
				this.m_deferredSortFlags = value;
			}
		}

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x0011C231 File Offset: 0x0011A431
		internal SortExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x060043C2 RID: 17346 RVA: 0x0011C239 File Offset: 0x0011A439
		// (set) Token: 0x060043C3 RID: 17347 RVA: 0x0011C241 File Offset: 0x0011A441
		internal bool NaturalSort
		{
			get
			{
				return this.m_naturalSort;
			}
			set
			{
				this.m_naturalSort = value;
			}
		}

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x060043C4 RID: 17348 RVA: 0x0011C24A File Offset: 0x0011A44A
		internal bool DeferredSort
		{
			get
			{
				return this.m_deferredSort;
			}
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x0011C252 File Offset: 0x0011A452
		internal bool ShouldApplySorting
		{
			get
			{
				return !this.m_naturalSort && !this.m_deferredSort && this.m_sortDirections != null && this.m_sortDirections.Count > 0;
			}
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x0011C27C File Offset: 0x0011A47C
		internal void ValidateNaturalSortFlags(PublishingContextStruct context)
		{
			this.m_naturalSort = Sorting.ValidateExclusiveSortFlag(context, this.m_naturalSortFlags, "NaturalSort");
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x0011C295 File Offset: 0x0011A495
		internal void ValidateDeferredSortFlags(PublishingContextStruct context)
		{
			this.m_deferredSort = Sorting.ValidateExclusiveSortFlag(context, this.m_deferredSortFlags, "DeferredSort");
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x0011C2B0 File Offset: 0x0011A4B0
		private static bool ValidateExclusiveSortFlag(PublishingContextStruct context, List<bool> flags, string propertyName)
		{
			if (flags == null || flags.Count == 0)
			{
				return false;
			}
			int count = flags.Count;
			bool flag = flags[0];
			for (int i = 1; i < count; i++)
			{
				if (flag != flags[i])
				{
					context.ErrorContext.Register(ProcessingErrorCode.rsInvalidSortFlagCombination, Severity.Error, context.ObjectType, context.ObjectName, propertyName, Array.Empty<string>());
					return false;
				}
			}
			return flag;
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x0011C31C File Offset: 0x0011A51C
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.SortStart();
			if (this.m_sortExpressions != null)
			{
				for (int i = 0; i < this.m_sortExpressions.Count; i++)
				{
					ExpressionInfo expressionInfo = this.m_sortExpressions[i];
					expressionInfo.Initialize("SortExpression", context);
					context.ExprHostBuilder.SortExpression(expressionInfo);
				}
			}
			context.ExprHostBuilder.SortEnd();
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x0011C385 File Offset: 0x0011A585
		internal void SetExprHost(SortExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null, "(exprHost != null && reportObjectModel != null)");
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x0011C3B4 File Offset: 0x0011A5B4
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			Sorting sorting = (Sorting)base.MemberwiseClone();
			if (this.m_sortExpressions != null)
			{
				sorting.m_sortExpressions = new List<ExpressionInfo>(this.m_sortExpressions.Count);
				foreach (ExpressionInfo expressionInfo in this.m_sortExpressions)
				{
					sorting.m_sortExpressions.Add((ExpressionInfo)expressionInfo.PublishClone(context));
				}
			}
			if (this.m_sortDirections != null)
			{
				sorting.m_sortDirections = new List<bool>(this.m_sortDirections.Count);
				foreach (bool flag in this.m_sortDirections)
				{
					sorting.m_sortDirections.Add(flag);
				}
			}
			return sorting;
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x0011C4AC File Offset: 0x0011A6AC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Sorting, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.SortExpressions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SortDirections, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Boolean),
				new MemberInfo(MemberName.NaturalSortFlags, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Boolean),
				new MemberInfo(MemberName.NaturalSort, Token.Boolean),
				new MemberInfo(MemberName.DeferredSortFlags, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Boolean, Lifetime.AddedIn(100)),
				new MemberInfo(MemberName.DeferredSort, Token.Boolean, Lifetime.AddedIn(100))
			});
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x0011C55C File Offset: 0x0011A75C
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Sorting.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.NaturalSort)
				{
					if (memberName == MemberName.SortDirections)
					{
						writer.WriteListOfPrimitives<bool>(this.m_sortDirections);
						continue;
					}
					if (memberName == MemberName.SortExpressions)
					{
						writer.Write<ExpressionInfo>(this.m_sortExpressions);
						continue;
					}
					if (memberName == MemberName.NaturalSort)
					{
						writer.Write(this.m_naturalSort);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.NaturalSortFlags)
					{
						writer.WriteListOfPrimitives<bool>(this.m_naturalSortFlags);
						continue;
					}
					if (memberName == MemberName.DeferredSortFlags)
					{
						writer.WriteListOfPrimitives<bool>(this.m_deferredSortFlags);
						continue;
					}
					if (memberName == MemberName.DeferredSort)
					{
						writer.Write(this.m_deferredSort);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x0011C634 File Offset: 0x0011A834
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Sorting.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.NaturalSort)
				{
					if (memberName == MemberName.SortDirections)
					{
						this.m_sortDirections = reader.ReadListOfPrimitives<bool>();
						continue;
					}
					if (memberName == MemberName.SortExpressions)
					{
						this.m_sortExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
						continue;
					}
					if (memberName == MemberName.NaturalSort)
					{
						this.m_naturalSort = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.NaturalSortFlags)
					{
						this.m_naturalSortFlags = reader.ReadListOfPrimitives<bool>();
						continue;
					}
					if (memberName == MemberName.DeferredSortFlags)
					{
						this.m_deferredSortFlags = reader.ReadListOfPrimitives<bool>();
						continue;
					}
					if (memberName == MemberName.DeferredSort)
					{
						this.m_deferredSort = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x0011C70C File Offset: 0x0011A90C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x0011C719 File Offset: 0x0011A919
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Sorting;
		}

		// Token: 0x04001ED3 RID: 7891
		private List<ExpressionInfo> m_sortExpressions;

		// Token: 0x04001ED4 RID: 7892
		private List<bool> m_sortDirections;

		// Token: 0x04001ED5 RID: 7893
		private List<bool> m_naturalSortFlags;

		// Token: 0x04001ED6 RID: 7894
		private bool m_naturalSort;

		// Token: 0x04001ED7 RID: 7895
		private List<bool> m_deferredSortFlags;

		// Token: 0x04001ED8 RID: 7896
		private bool m_deferredSort;

		// Token: 0x04001ED9 RID: 7897
		[NonSerialized]
		private SortExprHost m_exprHost;

		// Token: 0x04001EDA RID: 7898
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Sorting.GetDeclaration();
	}
}
