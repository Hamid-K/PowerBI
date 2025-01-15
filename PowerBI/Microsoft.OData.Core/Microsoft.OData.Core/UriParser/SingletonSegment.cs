using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AF RID: 431
	public sealed class SingletonSegment : ODataPathSegment
	{
		// Token: 0x0600145E RID: 5214 RVA: 0x0003B926 File Offset: 0x00039B26
		public SingletonSegment(IEdmSingleton singleton)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmSingleton>(singleton, "singleton");
			this.singleton = singleton;
			base.TargetEdmNavigationSource = singleton;
			base.TargetEdmType = singleton.EntityType();
			base.TargetKind = RequestTargetKind.Resource;
			base.SingleResult = true;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0003B962 File Offset: 0x00039B62
		public IEdmSingleton Singleton
		{
			get
			{
				return this.singleton;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x0003B96A File Offset: 0x00039B6A
		public override IEdmType EdmType
		{
			get
			{
				return this.singleton.EntityType();
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0003B977 File Offset: 0x00039B77
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0003B98C File Offset: 0x00039B8C
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0003B9A4 File Offset: 0x00039BA4
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			SingletonSegment singletonSegment = other as SingletonSegment;
			return singletonSegment != null && singletonSegment.singleton == this.Singleton;
		}

		// Token: 0x040008F3 RID: 2291
		private readonly IEdmSingleton singleton;
	}
}
