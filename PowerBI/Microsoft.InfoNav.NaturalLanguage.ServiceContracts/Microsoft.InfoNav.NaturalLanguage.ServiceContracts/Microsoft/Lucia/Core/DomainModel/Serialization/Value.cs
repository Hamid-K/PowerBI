using System;
using Microsoft.Lucia.Json;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C9 RID: 457
	public sealed class Value : IScalarForm<IValueList>, ICustomSerializationOptions
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00012523 File Offset: 0x00010723
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00012530 File Offset: 0x00010730
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public ValueList<string> Text
		{
			get
			{
				return this.Values as ValueList<string>;
			}
			set
			{
				this.Values = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00012539 File Offset: 0x00010739
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00012546 File Offset: 0x00010746
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, ItemConverterType = typeof(PrimitiveUnionConverter<long, double>))]
		public ValueList<Union<long, double>> Number
		{
			get
			{
				return this.Values as ValueList<Union<long, double>>;
			}
			set
			{
				this.Values = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001254F File Offset: 0x0001074F
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0001255C File Offset: 0x0001075C
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public ValueList<bool?> Boolean
		{
			get
			{
				return this.Values as ValueList<bool?>;
			}
			set
			{
				this.Values = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00012565 File Offset: 0x00010765
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x0001256D File Offset: 0x0001076D
		[JsonIgnore]
		internal IValueList Values
		{
			get
			{
				return this._values;
			}
			set
			{
				this._values = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00012576 File Offset: 0x00010776
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				IValueList values = this.Values;
				if (values == null)
				{
					return YamlSerializationOptions.None;
				}
				return values.Options;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00012589 File Offset: 0x00010789
		public override string ToString()
		{
			return JsonConvert.ToString(this);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00012591 File Offset: 0x00010791
		bool IScalarForm<IValueList>.TryGetScalarForm(out IValueList value)
		{
			value = this.Values;
			return true;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001259C File Offset: 0x0001079C
		void IScalarForm<IValueList>.SetFromScalarForm(IValueList value)
		{
			this.Values = value;
		}

		// Token: 0x040007C4 RID: 1988
		private IValueList _values;
	}
}
