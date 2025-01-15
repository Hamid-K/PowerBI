using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using Microsoft.OData.Client.Materialization;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client
{
	// Token: 0x020000D6 RID: 214
	internal class MaterializeAtom : IDisposable, IEnumerable, IEnumerator
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		internal MaterializeAtom(ResponseInfo responseInfo, QueryComponents queryComponents, ProjectionPlan plan, IODataResponseMessage responseMessage, ODataPayloadKind payloadKind)
		{
			this.responseInfo = responseInfo;
			this.elementType = queryComponents.LastSegmentType;
			this.expectingPrimitiveValue = PrimitiveType.IsKnownNullableType(this.elementType);
			Type type;
			Type typeForMaterializer = MaterializeAtom.GetTypeForMaterializer(this.expectingPrimitiveValue, this.elementType, responseInfo.Model, out type);
			this.materializer = ODataMaterializer.CreateMaterializerForMessage(responseMessage, responseInfo, typeForMaterializer, queryComponents, plan, payloadKind);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001CD38 File Offset: 0x0001AF38
		internal MaterializeAtom(ResponseInfo responseInfo, IEnumerable<ODataResource> entries, Type elementType, ODataFormat format)
		{
			this.responseInfo = responseInfo;
			this.elementType = elementType;
			this.expectingPrimitiveValue = PrimitiveType.IsKnownNullableType(elementType);
			Type type;
			Type typeForMaterializer = MaterializeAtom.GetTypeForMaterializer(this.expectingPrimitiveValue, this.elementType, responseInfo.Model, out type);
			QueryComponents queryComponents = new QueryComponents(null, Util.ODataVersionEmpty, elementType, null, null);
			ODataMaterializerContext odataMaterializerContext = new ODataMaterializerContext(responseInfo);
			EntityTrackingAdapter entityTrackingAdapter = new EntityTrackingAdapter(responseInfo.EntityTracker, responseInfo.MergeOption, responseInfo.Model, responseInfo.Context);
			this.materializer = new ODataEntriesEntityMaterializer(entries, odataMaterializerContext, entityTrackingAdapter, queryComponents, typeForMaterializer, null, format);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000347F File Offset: 0x0000167F
		private MaterializeAtom()
		{
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001CDC8 File Offset: 0x0001AFC8
		public object Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001CDDD File Offset: 0x0001AFDD
		internal static MaterializeAtom EmptyResults
		{
			get
			{
				return new MaterializeAtom.ResultsWrapper(null, null, null);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
		internal bool IsNoContentResponse()
		{
			MaterializeAtom.ResultsWrapper resultsWrapper = this as MaterializeAtom.ResultsWrapper;
			return resultsWrapper != null && resultsWrapper.IsEmptyResult();
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001CE07 File Offset: 0x0001B007
		internal bool IsCountable
		{
			get
			{
				return this.materializer != null && this.materializer.IsCountable;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001CE1E File Offset: 0x0001B01E
		internal virtual DataServiceContext Context
		{
			get
			{
				return this.responseInfo.Context;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001CE2B File Offset: 0x0001B02B
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0001CE42 File Offset: 0x0001B042
		internal Action<IDictionary<string, object>> SetInstanceAnnotations
		{
			get
			{
				if (this.materializer != null)
				{
					return this.materializer.SetInstanceAnnotations;
				}
				return null;
			}
			set
			{
				if (this.materializer != null)
				{
					this.materializer.SetInstanceAnnotations = value;
				}
			}
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001CE58 File Offset: 0x0001B058
		public void Dispose()
		{
			this.current = null;
			if (this.materializer != null)
			{
				this.materializer.Dispose();
			}
			if (this.writer != null)
			{
				this.writer.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001CE8D File Offset: 0x0001B08D
		public virtual IEnumerator GetEnumerator()
		{
			this.CheckGetEnumerator();
			return this;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001CE98 File Offset: 0x0001B098
		private static Type GetTypeForMaterializer(bool expectingPrimitiveValue, Type elementType, ClientEdmModel model, out Type implementationType)
		{
			if (!expectingPrimitiveValue && typeof(IEnumerable).IsAssignableFrom(elementType))
			{
				implementationType = ClientTypeUtil.GetImplementationType(elementType, typeof(ICollection<>));
				if (implementationType != null)
				{
					Type type = implementationType.GetGenericArguments()[0];
					if (ClientTypeUtil.TypeIsStructured(type, model))
					{
						return type;
					}
				}
			}
			implementationType = null;
			return elementType;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001CEF0 File Offset: 0x0001B0F0
		public bool MoveNext()
		{
			bool applyingChanges = this.responseInfo.ApplyingChanges;
			bool flag;
			try
			{
				this.responseInfo.ApplyingChanges = true;
				flag = this.MoveNextInternal();
			}
			finally
			{
				this.responseInfo.ApplyingChanges = applyingChanges;
			}
			return flag;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001CF3C File Offset: 0x0001B13C
		private bool MoveNextInternal()
		{
			if (this.materializer == null)
			{
				return false;
			}
			this.current = null;
			this.materializer.ClearLog();
			bool flag = false;
			Type type;
			MaterializeAtom.GetTypeForMaterializer(this.expectingPrimitiveValue, this.elementType, this.responseInfo.Model, out type);
			if (type != null)
			{
				if (this.moved)
				{
					return false;
				}
				Type type2 = type.GetGenericArguments()[0];
				type = this.elementType;
				if (type.IsInterface())
				{
					type = typeof(Collection<>).MakeGenericType(new Type[] { type2 });
				}
				IList list = (IList)Activator.CreateInstance(type);
				while (this.materializer.Read())
				{
					list.Add(this.materializer.CurrentValue);
				}
				this.moved = true;
				this.current = list;
				flag = true;
			}
			if (this.current == null)
			{
				if (this.expectingPrimitiveValue && this.moved)
				{
					flag = false;
				}
				else
				{
					flag = this.materializer.Read();
					if (flag)
					{
						BaseEntityType baseEntityType = this.materializer.CurrentValue as BaseEntityType;
						if (baseEntityType != null)
						{
							baseEntityType.Context = this.responseInfo.Context;
						}
						this.current = this.materializer.CurrentValue;
					}
					this.moved = true;
				}
			}
			this.materializer.ApplyLogToContext();
			return flag;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001D07F File Offset: 0x0001B27F
		void IEnumerator.Reset()
		{
			throw Error.NotSupported();
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001D086 File Offset: 0x0001B286
		internal static MaterializeAtom CreateWrapper(DataServiceContext context, IEnumerable results)
		{
			return new MaterializeAtom.ResultsWrapper(context, results, null);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001D090 File Offset: 0x0001B290
		internal static MaterializeAtom CreateWrapper(DataServiceContext context, IEnumerable results, DataServiceQueryContinuation continuation)
		{
			return new MaterializeAtom.ResultsWrapper(context, results, continuation);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001D09A File Offset: 0x0001B29A
		internal void SetInsertingObject(object addedObject)
		{
			((ODataEntityMaterializer)this.materializer).TargetInstance = addedObject;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001D0AD File Offset: 0x0001B2AD
		internal long CountValue()
		{
			return this.materializer.CountValue;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		internal virtual DataServiceQueryContinuation GetContinuation(IEnumerable key)
		{
			DataServiceQueryContinuation dataServiceQueryContinuation;
			if (key == null)
			{
				if ((this.expectingPrimitiveValue && !this.moved) || (!this.expectingPrimitiveValue && !this.materializer.IsEndOfStream))
				{
					throw new InvalidOperationException(Strings.MaterializeFromAtom_TopLevelLinkNotAvailable);
				}
				if (this.expectingPrimitiveValue || this.materializer.CurrentFeed == null)
				{
					dataServiceQueryContinuation = null;
				}
				else
				{
					dataServiceQueryContinuation = DataServiceQueryContinuation.Create(this.materializer.CurrentFeed.NextPageLink, this.materializer.MaterializeEntryPlan);
				}
			}
			else if (!this.materializer.NextLinkTable.TryGetValue(key, out dataServiceQueryContinuation))
			{
				throw new ArgumentException(Strings.MaterializeFromAtom_CollectionKeyNotPresentInLinkTable);
			}
			return dataServiceQueryContinuation;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001D159 File Offset: 0x0001B359
		private void CheckGetEnumerator()
		{
			if (this.calledGetEnumerator)
			{
				throw Error.NotSupported(Strings.Deserialize_GetEnumerator);
			}
			this.calledGetEnumerator = true;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001D178 File Offset: 0x0001B378
		internal static string ReadElementString(XmlReader reader, bool checkNullAttribute)
		{
			string text = null;
			bool flag = checkNullAttribute && !Util.DoesNullAttributeSayTrue(reader);
			if (!reader.IsEmptyElement)
			{
				while (reader.Read())
				{
					XmlNodeType nodeType = reader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					case XmlNodeType.Attribute:
						goto IL_0086;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					default:
						if (nodeType == XmlNodeType.Comment)
						{
							continue;
						}
						switch (nodeType)
						{
						case XmlNodeType.Whitespace:
							continue;
						case XmlNodeType.SignificantWhitespace:
							break;
						case XmlNodeType.EndElement:
						{
							string text2;
							if ((text2 = text) == null)
							{
								if (!flag)
								{
									return null;
								}
								text2 = string.Empty;
							}
							return text2;
						}
						default:
							goto IL_0086;
						}
						break;
					}
					if (text != null)
					{
						throw Error.InvalidOperation(Strings.Deserialize_MixedTextWithComment);
					}
					text = reader.Value;
					continue;
					IL_0086:
					throw Error.InvalidOperation(Strings.Deserialize_ExpectingSimpleValue);
				}
				throw Error.InvalidOperation(Strings.Deserialize_ExpectingSimpleValue);
			}
			if (!flag)
			{
				return null;
			}
			return string.Empty;
		}

		// Token: 0x04000323 RID: 803
		private readonly ResponseInfo responseInfo;

		// Token: 0x04000324 RID: 804
		private readonly Type elementType;

		// Token: 0x04000325 RID: 805
		private readonly bool expectingPrimitiveValue;

		// Token: 0x04000326 RID: 806
		private readonly ODataMaterializer materializer;

		// Token: 0x04000327 RID: 807
		private object current;

		// Token: 0x04000328 RID: 808
		private bool calledGetEnumerator;

		// Token: 0x04000329 RID: 809
		private bool moved;

		// Token: 0x0400032A RID: 810
		private TextWriter writer;

		// Token: 0x020001A3 RID: 419
		private class ResultsWrapper : MaterializeAtom
		{
			// Token: 0x06000EA4 RID: 3748 RVA: 0x000318FC File Offset: 0x0002FAFC
			internal ResultsWrapper(DataServiceContext context, IEnumerable results, DataServiceQueryContinuation continuation)
			{
				this.context = context;
				this.results = results ?? new object[0];
				this.continuation = continuation;
			}

			// Token: 0x17000385 RID: 901
			// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00031923 File Offset: 0x0002FB23
			internal override DataServiceContext Context
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x06000EA6 RID: 3750 RVA: 0x0003192B File Offset: 0x0002FB2B
			internal override DataServiceQueryContinuation GetContinuation(IEnumerable key)
			{
				if (key == null)
				{
					return this.continuation;
				}
				throw new InvalidOperationException(Strings.MaterializeFromAtom_GetNestLinkForFlatCollection);
			}

			// Token: 0x06000EA7 RID: 3751 RVA: 0x00031941 File Offset: 0x0002FB41
			internal bool IsEmptyResult()
			{
				return this.context == null && this.continuation == null;
			}

			// Token: 0x06000EA8 RID: 3752 RVA: 0x00031956 File Offset: 0x0002FB56
			public override IEnumerator GetEnumerator()
			{
				return this.results.GetEnumerator();
			}

			// Token: 0x040007A9 RID: 1961
			private readonly IEnumerable results;

			// Token: 0x040007AA RID: 1962
			private readonly DataServiceQueryContinuation continuation;

			// Token: 0x040007AB RID: 1963
			private readonly DataServiceContext context;
		}
	}
}
