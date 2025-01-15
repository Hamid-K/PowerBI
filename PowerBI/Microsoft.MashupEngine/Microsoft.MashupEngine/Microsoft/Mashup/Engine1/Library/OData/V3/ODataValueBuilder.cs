using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using Microsoft.Data.Edm;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.OData.V3.Compiler;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008E8 RID: 2280
	internal class ODataValueBuilder : ValueBuilderBase
	{
		// Token: 0x060040FA RID: 16634 RVA: 0x000D9278 File Offset: 0x000D7478
		public ODataValueBuilder(Query originalQuery, ODataEnvironment environment, ODataQueryPlan queryPlan)
			: this(originalQuery, environment, queryPlan, 0, null, false)
		{
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000D9299 File Offset: 0x000D7499
		public ODataValueBuilder(Query originalQuery, ODataEnvironment environment, ODataQueryPlan queryPlan, int skip, int? take, bool hasCount)
			: base(environment, queryPlan, skip, take, hasCount)
		{
			this.originalQuery = originalQuery;
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x000D92B0 File Offset: 0x000D74B0
		protected ODataEnvironment ODataEnvironment
		{
			get
			{
				return (ODataEnvironment)base.Environment;
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x000D92BD File Offset: 0x000D74BD
		protected Query OriginalQuery
		{
			get
			{
				return this.originalQuery;
			}
		}

		// Token: 0x170014E6 RID: 5350
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x000D92C5 File Offset: 0x000D74C5
		public ODataQueryPlan ODataQueryPlan
		{
			get
			{
				return (ODataQueryPlan)base.QueryPlan;
			}
		}

		// Token: 0x170014E7 RID: 5351
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x000D92D2 File Offset: 0x000D74D2
		protected string ExternalQuery
		{
			get
			{
				return this.RequestUri.AbsoluteUri;
			}
		}

		// Token: 0x170014E8 RID: 5352
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x000D92DF File Offset: 0x000D74DF
		protected Uri RequestUri
		{
			get
			{
				return this.RequestUris[0];
			}
		}

		// Token: 0x170014E9 RID: 5353
		// (get) Token: 0x06004101 RID: 16641 RVA: 0x000D92F0 File Offset: 0x000D74F0
		private IList<Uri> RequestUris
		{
			get
			{
				if (this.requestUris == null)
				{
					try
					{
						this.requestUris = this.ODataQueryPlan.CreateUris(this.ODataEnvironment.ServiceUri, (base.Skip <= 0) ? null : new int?(base.Skip), (base.Take == null) ? null : base.Take, base.HasCount);
					}
					catch (UriFormatException)
					{
						throw new FoldingFailureException(ODataErrors.QueryTooLong(this.ODataEnvironment.Host, this.ODataEnvironment.ServiceUri, this.ODataEnvironment.Resource.Kind));
					}
				}
				return this.requestUris;
			}
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x000D93B4 File Offset: 0x000D75B4
		protected virtual ODataValueBuilder CreateCountValueBuilder()
		{
			return new ODataValueBuilder(this.originalQuery, this.ODataEnvironment, this.ODataQueryPlan, base.Skip, base.Take, true);
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x000D93DC File Offset: 0x000D75DC
		public override long CreateCountOverEnumerator()
		{
			long num;
			try
			{
				double asDouble = this.CreateCountValueBuilder().GetSingleValue().AsNumber.AsDouble;
				num = (long)asDouble;
				if ((double)num != asDouble)
				{
					throw ValueException.ListCountTooLarge(num);
				}
			}
			catch (FoldingFailureException)
			{
				try
				{
					num = 0L;
					foreach (IValueReference valueReference in this)
					{
						num += 1L;
						if (num > ListValue.MaxCount)
						{
							throw ValueException.ListCountTooLarge(num);
						}
					}
				}
				catch (FoldingFailureException)
				{
					return this.originalQuery.RowCount.Value;
				}
			}
			if (num > ListValue.MaxCount)
			{
				throw ValueException.ListCountTooLarge(num);
			}
			return num;
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x000D94A4 File Offset: 0x000D76A4
		public override Value GetSingleValue()
		{
			Value value;
			using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					value = Value.Null;
				}
				else
				{
					value = enumerator.Current.Value;
				}
			}
			return value;
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x000D94F4 File Offset: 0x000D76F4
		protected virtual Value CreateEnumeratorListValue(ODataQueryPlan queryPlan)
		{
			IList<Uri> list = null;
			try
			{
				list = this.RequestUris;
			}
			catch (FoldingFailureException ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.ODataEnvironment.Host, "ODataValueBuilder/CreateEnumeratorListValue", TraceEventType.Information, this.ODataEnvironment.Resource))
				{
					hostTrace.Add("Folding failed with exception: ", ex, true);
					if (this.originalQuery != null)
					{
						return new ODataValue.OriginalQueryTableValue(this.originalQuery, queryPlan.Type);
					}
					throw;
				}
			}
			if (list.Count == 1)
			{
				return ODataValue.Create(this.ODataEnvironment, base.Type, list[0], this.originalQuery);
			}
			Value[] array = new Value[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				array[i] = ODataValue.Create(this.ODataEnvironment, base.Type, list[i], this.originalQuery);
			}
			if (array[0].Kind == ValueKind.Table)
			{
				return TableValue.Combine(ListValue.New(array), null);
			}
			return ListValue.Combine(ListValue.New(array));
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x000D9618 File Offset: 0x000D7818
		protected override ValueBuilderBase CreatePagingValueBuilder(Query originalQuery, int skipCount, int? takeCount)
		{
			return new ODataValueBuilder(originalQuery, this.ODataEnvironment, this.ODataQueryPlan, skipCount, takeCount, false);
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x000D9630 File Offset: 0x000D7830
		private Value CreateResultValue()
		{
			if (this.ODataQueryPlan.HasEmptyResult)
			{
				return ODataStructuralValueConverter.CreateDefaultValue(base.Type);
			}
			if (base.HasCount || this.ODataQueryPlan.HasValue)
			{
				return ListValue.New(new Value[] { this.CreateScalarValue(base.Type) });
			}
			return this.CreateEnumeratorListValue(this.ODataQueryPlan);
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x000D9692 File Offset: 0x000D7892
		protected virtual Value CreateScalarValue(TypeValue type)
		{
			return ODataValueBuilder.CreateScalarResult(this.RequestUris[0], this.ODataEnvironment, type);
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x000D96AC File Offset: 0x000D78AC
		public static Value CreateScalarResult(Uri requestUri, ODataEnvironment environment, TypeValue type)
		{
			string text = null;
			try
			{
				text = Http.GetScalarResponse(requestUri, environment);
			}
			catch (WebException ex)
			{
				throw new FoldingFailureException(ODataCommonErrors.RequestFailed(environment.Host, ex, requestUri, environment.HttpResource));
			}
			switch (ODataTypeServices.GetEdmTypeKind(type))
			{
			case EdmPrimitiveTypeKind.Binary:
				return BinaryValue.New(Convert.FromBase64String(text));
			case EdmPrimitiveTypeKind.Boolean:
				return LogicalValue.New(bool.Parse(text));
			case EdmPrimitiveTypeKind.Byte:
				return NumberValue.New((int)byte.Parse(text, CultureInfo.InvariantCulture)).ReplaceType(TypeValue.Byte);
			case EdmPrimitiveTypeKind.DateTime:
				return DateTimeValue.New(DateTime.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.DateTimeOffset:
				return DateTimeZoneValue.New(DateTimeOffset.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.Decimal:
				return NumberValue.New(decimal.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.Double:
				return NumberValue.New(double.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.Int16:
				return NumberValue.New((int)short.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.Int32:
				return NumberValue.New(int.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.Int64:
				return NumberValue.New(long.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.SByte:
				return NumberValue.New((int)sbyte.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.Single:
				return NumberValue.New((double)float.Parse(text, CultureInfo.InvariantCulture));
			case EdmPrimitiveTypeKind.Time:
			{
				TimeSpan timeSpan;
				if (TimeSpan.TryParse(text, out timeSpan))
				{
					return DurationValue.New(timeSpan);
				}
				return DurationValue.New(TimeSpan.Zero);
			}
			}
			if (type.TypeKind != ValueKind.Text)
			{
				throw new InvalidOperationException("Cannot create Value for response: " + text);
			}
			return TextValue.New(text);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x000D984C File Offset: 0x000D7A4C
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			Value value = this.CreateResultValue();
			IEnumerator<IValueReference> enumerator = (value.IsTable ? value.AsTable.GetEnumerator() : value.AsList.GetEnumerator());
			if (base.Take != null)
			{
				enumerator = new SkipTakeEnumerator<IValueReference>(enumerator, RowCount.Zero, new RowCount((long)base.Take.Value));
			}
			return enumerator;
		}

		// Token: 0x04002232 RID: 8754
		private readonly Query originalQuery;

		// Token: 0x04002233 RID: 8755
		private IList<Uri> requestUris;
	}
}
