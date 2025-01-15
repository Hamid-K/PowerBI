using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000266 RID: 614
	public sealed class SingletonSegment : ODataPathSegment
	{
		// Token: 0x0600159E RID: 5534 RVA: 0x0004BCE3 File Offset: 0x00049EE3
		public SingletonSegment(IEdmSingleton singleton)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmSingleton>(singleton, "singleton");
			this.singleton = singleton;
			base.TargetEdmNavigationSource = singleton;
			base.TargetEdmType = singleton.EntityType();
			base.TargetKind = RequestTargetKind.Resource;
			base.SingleResult = true;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0004BD1E File Offset: 0x00049F1E
		public IEdmSingleton Singleton
		{
			get
			{
				return this.singleton;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0004BD26 File Offset: 0x00049F26
		public override IEdmType EdmType
		{
			get
			{
				return this.singleton.EntityType();
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0004BD33 File Offset: 0x00049F33
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0004BD47 File Offset: 0x00049F47
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0004BD5C File Offset: 0x00049F5C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			SingletonSegment singletonSegment = other as SingletonSegment;
			return singletonSegment != null && singletonSegment.singleton == this.Singleton;
		}

		// Token: 0x040008FF RID: 2303
		private readonly IEdmSingleton singleton;
	}
}
