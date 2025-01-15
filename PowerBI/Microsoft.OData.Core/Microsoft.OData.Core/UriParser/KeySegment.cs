using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000185 RID: 389
	public sealed class KeySegment : ODataPathSegment
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x000392D8 File Offset: 0x000374D8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Using key value pair is exactly what we want here.")]
		public KeySegment(IEnumerable<KeyValuePair<string, object>> keys, IEdmEntityType edmType, IEdmNavigationSource navigationSource)
		{
			this.keys = new ReadOnlyCollection<KeyValuePair<string, object>>(keys.ToList<KeyValuePair<string, object>>());
			this.edmType = edmType;
			this.navigationSource = navigationSource;
			base.SingleResult = true;
			if (navigationSource != null)
			{
				ExceptionUtil.ThrowIfTypesUnrelated(edmType, navigationSource.EntityType(), "KeySegments");
			}
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00039325 File Offset: 0x00037525
		public KeySegment(ODataPathSegment previous, IEnumerable<KeyValuePair<string, object>> keys, IEdmEntityType edmType, IEdmNavigationSource navigationSource)
			: this(keys, edmType, navigationSource)
		{
			if (previous != null)
			{
				base.CopyValuesFrom(previous);
				base.SingleResult = true;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00039342 File Offset: 0x00037542
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Using key value pair is exactly what we want here.")]
		public IEnumerable<KeyValuePair<string, object>> Keys
		{
			get
			{
				return this.keys.AsEnumerable<KeyValuePair<string, object>>();
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0003934F File Offset: 0x0003754F
		public override IEdmType EdmType
		{
			get
			{
				return this.edmType;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00039357 File Offset: 0x00037557
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0003935F File Offset: 0x0003755F
		public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentTranslator<T>>(translator, "translator");
			return translator.Translate(this);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00039374 File Offset: 0x00037574
		public override void HandleWith(PathSegmentHandler handler)
		{
			ExceptionUtils.CheckArgumentNotNull<PathSegmentHandler>(handler, "handler");
			handler.Handle(this);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0003938C File Offset: 0x0003758C
		internal override bool Equals(ODataPathSegment other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataPathSegment>(other, "other");
			KeySegment keySegment = other as KeySegment;
			return keySegment != null && keySegment.Keys.SequenceEqual(this.Keys) && keySegment.EdmType == this.edmType && keySegment.NavigationSource == this.navigationSource;
		}

		// Token: 0x04000897 RID: 2199
		private readonly ReadOnlyCollection<KeyValuePair<string, object>> keys;

		// Token: 0x04000898 RID: 2200
		private readonly IEdmEntityType edmType;

		// Token: 0x04000899 RID: 2201
		private readonly IEdmNavigationSource navigationSource;
	}
}
