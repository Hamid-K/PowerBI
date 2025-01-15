using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000070 RID: 112
	[DebuggerDisplay("[DataShape] Id={Id}")]
	internal sealed class DataShape : IScope, IContextItem, IIdentifiable, IDataBoundItem
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000613B File Offset: 0x0000433B
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00006143 File Offset: 0x00004343
		public Identifier Id { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000614C File Offset: 0x0000434C
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00006154 File Offset: 0x00004354
		public Identifier DataSourceId { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000615D File Offset: 0x0000435D
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00006165 File Offset: 0x00004365
		public DataShapeUsage Usage { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000616E File Offset: 0x0000436E
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00006176 File Offset: 0x00004376
		public ExtensionSchema ExtensionSchema { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000617F File Offset: 0x0000437F
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00006187 File Offset: 0x00004387
		public string DataSourceVariables { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00006190 File Offset: 0x00004390
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00006198 File Offset: 0x00004398
		public List<ModelParameter> ModelParameters { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000061A1 File Offset: 0x000043A1
		// (set) Token: 0x06000283 RID: 643 RVA: 0x000061A9 File Offset: 0x000043A9
		public DataHierarchy PrimaryHierarchy { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000061B2 File Offset: 0x000043B2
		// (set) Token: 0x06000285 RID: 645 RVA: 0x000061BA File Offset: 0x000043BA
		public DataHierarchy SecondaryHierarchy { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000061C3 File Offset: 0x000043C3
		// (set) Token: 0x06000287 RID: 647 RVA: 0x000061CB File Offset: 0x000043CB
		public List<DataRow> DataRows { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000288 RID: 648 RVA: 0x000061D4 File Offset: 0x000043D4
		// (set) Token: 0x06000289 RID: 649 RVA: 0x000061DC File Offset: 0x000043DC
		public List<Calculation> Calculations { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000061E5 File Offset: 0x000043E5
		// (set) Token: 0x0600028B RID: 651 RVA: 0x000061ED File Offset: 0x000043ED
		public List<DataShape> DataShapes { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000061F6 File Offset: 0x000043F6
		// (set) Token: 0x0600028D RID: 653 RVA: 0x000061FE File Offset: 0x000043FE
		public List<Filter> Filters { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00006207 File Offset: 0x00004407
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000620F File Offset: 0x0000440F
		public List<Limit> Limits { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00006218 File Offset: 0x00004418
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00006220 File Offset: 0x00004420
		public DynamicLimits DynamicLimits { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00006229 File Offset: 0x00004429
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00006231 File Offset: 0x00004431
		public Candidate<int> RequestedPrimaryLeafCount { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000623A File Offset: 0x0000443A
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00006242 File Offset: 0x00004442
		public Candidate<bool> ContextOnly { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000624B File Offset: 0x0000444B
		public ObjectType ObjectType
		{
			get
			{
				return ObjectType.DataShape;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000624F File Offset: 0x0000444F
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00006257 File Offset: 0x00004457
		public List<RestartToken> RestartTokens { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00006260 File Offset: 0x00004460
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00006268 File Offset: 0x00004468
		public Candidate<bool> IncludeRestartToken { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00006271 File Offset: 0x00004471
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00006279 File Offset: 0x00004479
		public RestartMatchingBehavior? RestartMatchingBehavior { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00006282 File Offset: 0x00004482
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000628A File Offset: 0x0000448A
		public List<DataTransform> Transforms { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00006293 File Offset: 0x00004493
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000629B File Offset: 0x0000449B
		public List<Message> Messages { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x000062A4 File Offset: 0x000044A4
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x000062AC File Offset: 0x000044AC
		public bool IsIndependent { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x000062B5 File Offset: 0x000044B5
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x000062BD File Offset: 0x000044BD
		public List<QueryParameterDeclaration> QueryParameters { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000062C6 File Offset: 0x000044C6
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x000062CE File Offset: 0x000044CE
		public List<VisualAxis> VisualCalculationMetadata { get; set; }
	}
}
