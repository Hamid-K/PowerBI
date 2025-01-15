using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200049C RID: 1180
	[Serializable]
	internal sealed class ChartDataPointValues : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060038E2 RID: 14562 RVA: 0x000F7A39 File Offset: 0x000F5C39
		internal ChartDataPointValues()
		{
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000F7A41 File Offset: 0x000F5C41
		internal ChartDataPointValues(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart, Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint dataPoint)
		{
			this.m_dataPoint = dataPoint;
			this.m_chart = chart;
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x000F7A57 File Offset: 0x000F5C57
		// (set) Token: 0x060038E5 RID: 14565 RVA: 0x000F7A5F File Offset: 0x000F5C5F
		internal ExpressionInfo X
		{
			get
			{
				return this.m_x;
			}
			set
			{
				this.m_x = value;
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000F7A68 File Offset: 0x000F5C68
		// (set) Token: 0x060038E7 RID: 14567 RVA: 0x000F7A70 File Offset: 0x000F5C70
		internal ExpressionInfo Y
		{
			get
			{
				return this.m_y;
			}
			set
			{
				this.m_y = value;
			}
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x060038E8 RID: 14568 RVA: 0x000F7A79 File Offset: 0x000F5C79
		// (set) Token: 0x060038E9 RID: 14569 RVA: 0x000F7A81 File Offset: 0x000F5C81
		internal ExpressionInfo Size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x170018CD RID: 6349
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x000F7A8A File Offset: 0x000F5C8A
		// (set) Token: 0x060038EB RID: 14571 RVA: 0x000F7A92 File Offset: 0x000F5C92
		internal ExpressionInfo High
		{
			get
			{
				return this.m_high;
			}
			set
			{
				this.m_high = value;
			}
		}

		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000F7A9B File Offset: 0x000F5C9B
		// (set) Token: 0x060038ED RID: 14573 RVA: 0x000F7AA3 File Offset: 0x000F5CA3
		internal ExpressionInfo Low
		{
			get
			{
				return this.m_low;
			}
			set
			{
				this.m_low = value;
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000F7AAC File Offset: 0x000F5CAC
		// (set) Token: 0x060038EF RID: 14575 RVA: 0x000F7AB4 File Offset: 0x000F5CB4
		internal ExpressionInfo Start
		{
			get
			{
				return this.m_start;
			}
			set
			{
				this.m_start = value;
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000F7ABD File Offset: 0x000F5CBD
		// (set) Token: 0x060038F1 RID: 14577 RVA: 0x000F7AC5 File Offset: 0x000F5CC5
		internal ExpressionInfo End
		{
			get
			{
				return this.m_end;
			}
			set
			{
				this.m_end = value;
			}
		}

		// Token: 0x170018D1 RID: 6353
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000F7ACE File Offset: 0x000F5CCE
		// (set) Token: 0x060038F3 RID: 14579 RVA: 0x000F7AD6 File Offset: 0x000F5CD6
		internal ExpressionInfo Mean
		{
			get
			{
				return this.m_mean;
			}
			set
			{
				this.m_mean = value;
			}
		}

		// Token: 0x170018D2 RID: 6354
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000F7ADF File Offset: 0x000F5CDF
		// (set) Token: 0x060038F5 RID: 14581 RVA: 0x000F7AE7 File Offset: 0x000F5CE7
		internal ExpressionInfo Median
		{
			get
			{
				return this.m_median;
			}
			set
			{
				this.m_median = value;
			}
		}

		// Token: 0x170018D3 RID: 6355
		// (set) Token: 0x060038F6 RID: 14582 RVA: 0x000F7AF0 File Offset: 0x000F5CF0
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint DataPoint
		{
			set
			{
				this.m_dataPoint = value;
			}
		}

		// Token: 0x170018D4 RID: 6356
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000F7AF9 File Offset: 0x000F5CF9
		// (set) Token: 0x060038F8 RID: 14584 RVA: 0x000F7B01 File Offset: 0x000F5D01
		internal ExpressionInfo HighlightX
		{
			get
			{
				return this.m_highlightX;
			}
			set
			{
				this.m_highlightX = value;
			}
		}

		// Token: 0x170018D5 RID: 6357
		// (get) Token: 0x060038F9 RID: 14585 RVA: 0x000F7B0A File Offset: 0x000F5D0A
		// (set) Token: 0x060038FA RID: 14586 RVA: 0x000F7B12 File Offset: 0x000F5D12
		internal ExpressionInfo HighlightY
		{
			get
			{
				return this.m_highlightY;
			}
			set
			{
				this.m_highlightY = value;
			}
		}

		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x060038FB RID: 14587 RVA: 0x000F7B1B File Offset: 0x000F5D1B
		// (set) Token: 0x060038FC RID: 14588 RVA: 0x000F7B23 File Offset: 0x000F5D23
		internal ExpressionInfo HighlightSize
		{
			get
			{
				return this.m_highlightSize;
			}
			set
			{
				this.m_highlightSize = value;
			}
		}

		// Token: 0x170018D7 RID: 6359
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x000F7B2C File Offset: 0x000F5D2C
		// (set) Token: 0x060038FE RID: 14590 RVA: 0x000F7B34 File Offset: 0x000F5D34
		internal ExpressionInfo FormatX
		{
			get
			{
				return this.m_formatX;
			}
			set
			{
				this.m_formatX = value;
			}
		}

		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000F7B3D File Offset: 0x000F5D3D
		// (set) Token: 0x06003900 RID: 14592 RVA: 0x000F7B45 File Offset: 0x000F5D45
		internal ExpressionInfo FormatY
		{
			get
			{
				return this.m_formatY;
			}
			set
			{
				this.m_formatY = value;
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000F7B4E File Offset: 0x000F5D4E
		// (set) Token: 0x06003902 RID: 14594 RVA: 0x000F7B56 File Offset: 0x000F5D56
		internal ExpressionInfo FormatSize
		{
			get
			{
				return this.m_formatSize;
			}
			set
			{
				this.m_formatSize = value;
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000F7B5F File Offset: 0x000F5D5F
		// (set) Token: 0x06003904 RID: 14596 RVA: 0x000F7B67 File Offset: 0x000F5D67
		internal ExpressionInfo CurrencyLanguageX
		{
			get
			{
				return this.m_currencyLanguageX;
			}
			set
			{
				this.m_currencyLanguageX = value;
			}
		}

		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x000F7B70 File Offset: 0x000F5D70
		// (set) Token: 0x06003906 RID: 14598 RVA: 0x000F7B78 File Offset: 0x000F5D78
		internal ExpressionInfo CurrencyLanguageY
		{
			get
			{
				return this.m_currencyLanguageY;
			}
			set
			{
				this.m_currencyLanguageY = value;
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x000F7B81 File Offset: 0x000F5D81
		// (set) Token: 0x06003908 RID: 14600 RVA: 0x000F7B89 File Offset: 0x000F5D89
		internal ExpressionInfo CurrencyLanguageSize
		{
			get
			{
				return this.m_currencyLanguageSize;
			}
			set
			{
				this.m_currencyLanguageSize = value;
			}
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000F7B94 File Offset: 0x000F5D94
		internal void Initialize(InitializationContext context)
		{
			if (this.m_x != null)
			{
				this.m_x.Initialize("X", context);
				context.ExprHostBuilder.ChartDataPointValueX(this.m_x);
			}
			if (this.m_y != null)
			{
				this.m_y.Initialize("Y", context);
				context.ExprHostBuilder.ChartDataPointValueY(this.m_y);
			}
			if (this.m_size != null)
			{
				this.m_size.Initialize("Size", context);
				context.ExprHostBuilder.ChartDataPointValueSize(this.m_size);
			}
			if (this.m_high != null)
			{
				this.m_high.Initialize("High", context);
				context.ExprHostBuilder.ChartDataPointValueHigh(this.m_high);
			}
			if (this.m_low != null)
			{
				this.m_low.Initialize("Low", context);
				context.ExprHostBuilder.ChartDataPointValueLow(this.m_low);
			}
			if (this.m_start != null)
			{
				this.m_start.Initialize("Start", context);
				context.ExprHostBuilder.ChartDataPointValueStart(this.m_start);
			}
			if (this.m_end != null)
			{
				this.m_end.Initialize("End", context);
				context.ExprHostBuilder.ChartDataPointValueEnd(this.m_end);
			}
			if (this.m_mean != null)
			{
				this.m_mean.Initialize("Mean", context);
				context.ExprHostBuilder.ChartDataPointValueMean(this.m_mean);
			}
			if (this.m_median != null)
			{
				this.m_median.Initialize("Median", context);
				context.ExprHostBuilder.ChartDataPointValueMedian(this.m_median);
			}
			if (this.m_highlightX != null)
			{
				this.m_highlightX.Initialize("HighlightX", context);
				context.ExprHostBuilder.ChartDataPointValueHighlightX(this.m_highlightX);
			}
			if (this.m_highlightY != null)
			{
				this.m_highlightY.Initialize("HighlightY", context);
				context.ExprHostBuilder.ChartDataPointValueHighlightY(this.m_highlightY);
			}
			if (this.m_highlightSize != null)
			{
				this.m_highlightSize.Initialize("HighlightSize", context);
				context.ExprHostBuilder.ChartDataPointValueHighlightSize(this.m_highlightSize);
			}
			if (this.m_formatX != null)
			{
				this.m_formatX.Initialize("FormatX", context);
				context.ExprHostBuilder.ChartDataPointValueFormatX(this.m_formatX);
			}
			if (this.m_formatY != null)
			{
				this.m_formatY.Initialize("FormatY", context);
				context.ExprHostBuilder.ChartDataPointValueFormatY(this.m_formatY);
			}
			if (this.m_formatSize != null)
			{
				this.m_formatSize.Initialize("FormatSize", context);
				context.ExprHostBuilder.ChartDataPointValueFormatSize(this.m_formatSize);
			}
			if (this.m_currencyLanguageX != null)
			{
				this.m_currencyLanguageX.Initialize("CurrencyLanguageX", context);
				context.ExprHostBuilder.ChartDataPointValueCurrencyLanguageX(this.m_currencyLanguageX);
			}
			if (this.m_currencyLanguageY != null)
			{
				this.m_currencyLanguageY.Initialize("CurrencyLanguageY", context);
				context.ExprHostBuilder.ChartDataPointValueCurrencyLanguageY(this.m_currencyLanguageY);
			}
			if (this.m_currencyLanguageSize != null)
			{
				this.m_currencyLanguageSize.Initialize("CurrencyLanguageSize", context);
				context.ExprHostBuilder.ChartDataPointValueCurrencyLanguageSize(this.m_currencyLanguageSize);
			}
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000F7EA8 File Offset: 0x000F60A8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPointValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.X, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Y, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Size, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.High, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Low, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Start, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.End, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Mean, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Median, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartDataPoint, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPoint, Token.Reference),
				new MemberInfo(MemberName.Chart, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Chart, Token.Reference),
				new MemberInfo(MemberName.HighlightX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HighlightY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HighlightSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.FormatX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.FormatY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.FormatSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.CurrencyLanguageX, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.CurrencyLanguageY, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.CurrencyLanguageSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Lifetime.AddedIn(200))
			});
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000F80AC File Offset: 0x000F62AC
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ChartDataPointValues.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartDataPoint)
				{
					if (memberName <= MemberName.Median)
					{
						if (memberName == MemberName.Size)
						{
							writer.Write(this.m_size);
							continue;
						}
						switch (memberName)
						{
						case MemberName.X:
							writer.Write(this.m_x);
							continue;
						case MemberName.Y:
							writer.Write(this.m_y);
							continue;
						case MemberName.High:
							writer.Write(this.m_high);
							continue;
						case MemberName.Low:
							writer.Write(this.m_low);
							continue;
						case MemberName.Start:
							writer.Write(this.m_start);
							continue;
						case MemberName.End:
							writer.Write(this.m_end);
							continue;
						case MemberName.Mean:
							writer.Write(this.m_mean);
							continue;
						case MemberName.Median:
							writer.Write(this.m_median);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Chart)
						{
							writer.WriteReference(this.m_chart);
							continue;
						}
						if (memberName == MemberName.ChartDataPoint)
						{
							writer.WriteReference(this.m_dataPoint);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.HighlightX)
				{
					if (memberName == MemberName.HighlightY)
					{
						writer.Write(this.m_highlightY);
						continue;
					}
					if (memberName == MemberName.HighlightX)
					{
						writer.Write(this.m_highlightX);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HighlightSize)
					{
						writer.Write(this.m_highlightSize);
						continue;
					}
					switch (memberName)
					{
					case MemberName.FormatX:
						writer.Write(this.m_formatX);
						continue;
					case MemberName.FormatY:
						writer.Write(this.m_formatY);
						continue;
					case MemberName.FormatSize:
						writer.Write(this.m_formatSize);
						continue;
					case MemberName.CurrencyLanguageX:
						writer.Write(this.m_currencyLanguageX);
						continue;
					case MemberName.CurrencyLanguageY:
						writer.Write(this.m_currencyLanguageY);
						continue;
					case MemberName.CurrencyLanguageSize:
						writer.Write(this.m_currencyLanguageSize);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000F82FC File Offset: 0x000F64FC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ChartDataPointValues.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ChartDataPoint)
				{
					if (memberName <= MemberName.Median)
					{
						if (memberName == MemberName.Size)
						{
							this.m_size = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						switch (memberName)
						{
						case MemberName.X:
							this.m_x = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Y:
							this.m_y = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.High:
							this.m_high = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Low:
							this.m_low = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Start:
							this.m_start = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.End:
							this.m_end = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Mean:
							this.m_mean = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Median:
							this.m_median = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Chart)
						{
							this.m_chart = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Chart>(this);
							continue;
						}
						if (memberName == MemberName.ChartDataPoint)
						{
							this.m_dataPoint = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint>(this);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.HighlightX)
				{
					if (memberName == MemberName.HighlightY)
					{
						this.m_highlightY = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.HighlightX)
					{
						this.m_highlightX = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HighlightSize)
					{
						this.m_highlightSize = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.FormatX:
						this.m_formatX = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.FormatY:
						this.m_formatY = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.FormatSize:
						this.m_formatSize = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CurrencyLanguageX:
						this.m_currencyLanguageX = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CurrencyLanguageY:
						this.m_currencyLanguageY = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CurrencyLanguageSize:
						this.m_currencyLanguageSize = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000F85AC File Offset: 0x000F67AC
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ChartDataPointValues.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName != MemberName.Chart)
					{
						if (memberName == MemberName.ChartDataPoint)
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
							this.m_dataPoint = (Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint)referenceableItems[memberReference.RefID];
						}
						else
						{
							Global.Tracer.Assert(false);
						}
					}
					else
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						this.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)referenceableItems[memberReference.RefID];
					}
				}
			}
		}

		// Token: 0x0600390E RID: 14606 RVA: 0x000F8690 File Offset: 0x000F6890
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartDataPointValues;
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000F8698 File Offset: 0x000F6898
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ChartDataPointValues chartDataPointValues = (ChartDataPointValues)base.MemberwiseClone();
			chartDataPointValues.m_chart = (Microsoft.ReportingServices.ReportIntermediateFormat.Chart)context.CurrentDataRegionClone;
			if (this.m_x != null)
			{
				chartDataPointValues.m_x = (ExpressionInfo)this.m_x.PublishClone(context);
			}
			if (this.m_y != null)
			{
				chartDataPointValues.m_y = (ExpressionInfo)this.m_y.PublishClone(context);
			}
			if (this.m_size != null)
			{
				chartDataPointValues.m_size = (ExpressionInfo)this.m_size.PublishClone(context);
			}
			if (this.m_high != null)
			{
				chartDataPointValues.m_high = (ExpressionInfo)this.m_high.PublishClone(context);
			}
			if (this.m_low != null)
			{
				chartDataPointValues.m_low = (ExpressionInfo)this.m_low.PublishClone(context);
			}
			if (this.m_start != null)
			{
				chartDataPointValues.m_start = (ExpressionInfo)this.m_start.PublishClone(context);
			}
			if (this.m_end != null)
			{
				chartDataPointValues.m_end = (ExpressionInfo)this.m_end.PublishClone(context);
			}
			if (this.m_mean != null)
			{
				chartDataPointValues.m_mean = (ExpressionInfo)this.m_mean.PublishClone(context);
			}
			if (this.m_median != null)
			{
				chartDataPointValues.m_median = (ExpressionInfo)this.m_median.PublishClone(context);
			}
			return chartDataPointValues;
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000F87DB File Offset: 0x000F69DB
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesXExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000F8806 File Offset: 0x000F6A06
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesYExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000F8831 File Offset: 0x000F6A31
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValueSizesExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000F885C File Offset: 0x000F6A5C
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateHigh(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesHighExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000F8887 File Offset: 0x000F6A87
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateLow(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesLowExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000F88B2 File Offset: 0x000F6AB2
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateStart(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesStartExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000F88DD File Offset: 0x000F6ADD
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateEnd(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesEndExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000F8908 File Offset: 0x000F6B08
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateMean(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesMeanExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000F8933 File Offset: 0x000F6B33
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateMedian(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesMedianExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000F895E File Offset: 0x000F6B5E
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateHighlightX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesHighlightXExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000F8989 File Offset: 0x000F6B89
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateHighlightY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesHighlightYExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000F89B4 File Offset: 0x000F6BB4
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateHighlightSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesHighlightSizeExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000F89DF File Offset: 0x000F6BDF
		internal string EvaluateFormatX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesFormatXExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000F8A0A File Offset: 0x000F6C0A
		internal string EvaluateFormatY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesFormatYExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000F8A35 File Offset: 0x000F6C35
		internal string EvaluateFormatSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesFormatSizeExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000F8A60 File Offset: 0x000F6C60
		internal string EvaluateCurrencyLanguageX(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesCurrencyLanguageXExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000F8A8B File Offset: 0x000F6C8B
		internal string EvaluateCurrencyLanguageY(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesCurrencyLanguageYExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000F8AB6 File Offset: 0x000F6CB6
		internal string EvaluateCurrencyLanguageSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_dataPoint, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartDataPointValuesCurrencyLanguageSizeExpression(this.m_dataPoint, this.m_chart.Name);
		}

		// Token: 0x04001B69 RID: 7017
		private ExpressionInfo m_x;

		// Token: 0x04001B6A RID: 7018
		private ExpressionInfo m_y;

		// Token: 0x04001B6B RID: 7019
		private ExpressionInfo m_size;

		// Token: 0x04001B6C RID: 7020
		private ExpressionInfo m_high;

		// Token: 0x04001B6D RID: 7021
		private ExpressionInfo m_low;

		// Token: 0x04001B6E RID: 7022
		private ExpressionInfo m_start;

		// Token: 0x04001B6F RID: 7023
		private ExpressionInfo m_end;

		// Token: 0x04001B70 RID: 7024
		private ExpressionInfo m_mean;

		// Token: 0x04001B71 RID: 7025
		private ExpressionInfo m_median;

		// Token: 0x04001B72 RID: 7026
		private ExpressionInfo m_highlightX;

		// Token: 0x04001B73 RID: 7027
		private ExpressionInfo m_highlightY;

		// Token: 0x04001B74 RID: 7028
		private ExpressionInfo m_highlightSize;

		// Token: 0x04001B75 RID: 7029
		private ExpressionInfo m_formatX;

		// Token: 0x04001B76 RID: 7030
		private ExpressionInfo m_formatY;

		// Token: 0x04001B77 RID: 7031
		private ExpressionInfo m_formatSize;

		// Token: 0x04001B78 RID: 7032
		private ExpressionInfo m_currencyLanguageX;

		// Token: 0x04001B79 RID: 7033
		private ExpressionInfo m_currencyLanguageY;

		// Token: 0x04001B7A RID: 7034
		private ExpressionInfo m_currencyLanguageSize;

		// Token: 0x04001B7B RID: 7035
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.ChartDataPoint m_dataPoint;

		// Token: 0x04001B7C RID: 7036
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Chart m_chart;

		// Token: 0x04001B7D RID: 7037
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartDataPointValues.GetDeclaration();
	}
}
