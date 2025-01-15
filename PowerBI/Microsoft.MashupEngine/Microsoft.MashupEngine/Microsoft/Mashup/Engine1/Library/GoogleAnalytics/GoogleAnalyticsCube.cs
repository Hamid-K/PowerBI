using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AFA RID: 2810
	internal class GoogleAnalyticsCube : IGoogleAnalyticsCube
	{
		// Token: 0x06004DF5 RID: 19957 RVA: 0x00102992 File Offset: 0x00100B92
		public GoogleAnalyticsCube(IGoogleAnalyticsService service, GoogleAnalyticsProperty property, string viewId, string name, DateTime createdDate, Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>> resultEnumeratorFactory)
		{
			this.service = service;
			this.property = property;
			this.viewId = viewId;
			this.name = name;
			this.createdDate = createdDate;
			this.resultEnumeratorFactory = resultEnumeratorFactory;
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x001029C7 File Offset: 0x00100BC7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x001029CF File Offset: 0x00100BCF
		public string ViewId
		{
			get
			{
				return this.viewId;
			}
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x06004DF8 RID: 19960 RVA: 0x001029D7 File Offset: 0x00100BD7
		public IList<GoogleAnalyticsCubeObject> Dimensions
		{
			get
			{
				this.EnsureInitialized();
				return this.dimensions;
			}
		}

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x001029E5 File Offset: 0x00100BE5
		public IList<GoogleAnalyticsCubeObject> Measures
		{
			get
			{
				this.EnsureInitialized();
				return this.measures;
			}
		}

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x001029F3 File Offset: 0x00100BF3
		public DateTime Created
		{
			get
			{
				return this.createdDate;
			}
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x06004DFB RID: 19963 RVA: 0x001029FB File Offset: 0x00100BFB
		public DateTime FixedNow
		{
			get
			{
				return this.service.GetFixedNow();
			}
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x00102A08 File Offset: 0x00100C08
		public IEngineHost Host
		{
			get
			{
				return this.service.Host;
			}
		}

		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x00102A15 File Offset: 0x00100C15
		public Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>> ResultEnumeratorFactory
		{
			get
			{
				return this.resultEnumeratorFactory;
			}
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x00102A1D File Offset: 0x00100C1D
		public GoogleAnalyticsCubeObject GetObject(string id)
		{
			this.EnsureInitialized();
			return this.objects[id];
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x00102A31 File Offset: 0x00100C31
		public Value Query(GoogleAnalyticsQueryExpression compiledExpression)
		{
			return this.service.GetReport(this.viewId, compiledExpression);
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x00102A48 File Offset: 0x00100C48
		private void EnsureInitialized()
		{
			if (!this.isInitialized)
			{
				this.service.DownloadMetadata(this.property, out this.measures, out this.dimensions);
				this.objects = new Dictionary<string, GoogleAnalyticsCubeObject>(this.measures.Count + this.dimensions.Count);
				foreach (GoogleAnalyticsCubeObject googleAnalyticsCubeObject in this.measures.Concat(this.dimensions))
				{
					this.objects[googleAnalyticsCubeObject.ID] = googleAnalyticsCubeObject;
				}
				this.isInitialized = true;
			}
		}

		// Token: 0x040029CF RID: 10703
		private readonly IGoogleAnalyticsService service;

		// Token: 0x040029D0 RID: 10704
		private readonly GoogleAnalyticsProperty property;

		// Token: 0x040029D1 RID: 10705
		private readonly string name;

		// Token: 0x040029D2 RID: 10706
		private readonly string viewId;

		// Token: 0x040029D3 RID: 10707
		private readonly DateTime createdDate;

		// Token: 0x040029D4 RID: 10708
		private readonly Func<GoogleAnalyticsCube, GoogleAnalyticsQueryExpression, CubeExpression, Keys, IEnumerator<IValueReference>> resultEnumeratorFactory;

		// Token: 0x040029D5 RID: 10709
		private IList<GoogleAnalyticsCubeObject> measures;

		// Token: 0x040029D6 RID: 10710
		private IList<GoogleAnalyticsCubeObject> dimensions;

		// Token: 0x040029D7 RID: 10711
		private Dictionary<string, GoogleAnalyticsCubeObject> objects;

		// Token: 0x040029D8 RID: 10712
		private bool isInitialized;
	}
}
