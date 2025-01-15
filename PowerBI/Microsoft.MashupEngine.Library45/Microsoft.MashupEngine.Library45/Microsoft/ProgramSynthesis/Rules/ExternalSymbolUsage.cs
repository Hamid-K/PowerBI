using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x02000381 RID: 897
	[DataContract]
	public class ExternalSymbolUsage
	{
		// Token: 0x06001401 RID: 5121 RVA: 0x0003A9DE File Offset: 0x00038BDE
		public ExternalSymbolUsage(Dictionary<Symbol, Symbol> substitutions = null)
		{
			this.Substitutions = substitutions ?? new Dictionary<Symbol, Symbol>();
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0003A9F6 File Offset: 0x00038BF6
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x0003A9FE File Offset: 0x00038BFE
		[DataMember]
		public Dictionary<Symbol, Symbol> Substitutions { get; private set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0003AA08 File Offset: 0x00038C08
		private ConcurrentDictionary<FeatureInfo, Func<object, object>> FeatureConverters
		{
			get
			{
				ConcurrentDictionary<FeatureInfo, Func<object, object>> concurrentDictionary;
				if ((concurrentDictionary = this._featureConverters) == null)
				{
					concurrentDictionary = (this._featureConverters = new ConcurrentDictionary<FeatureInfo, Func<object, object>>());
				}
				return concurrentDictionary;
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0003AA30 File Offset: 0x00038C30
		internal object ConvertFromExternFeatureValue(FeatureInfo localFeature, FeatureInfo externFeature, object externValue)
		{
			Func<object, object> func;
			if (this.FeatureConverters.TryGetValue(localFeature, out func))
			{
				return func(externValue);
			}
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object));
			Expression expression = Expression.Convert(Expression.Convert(Expression.Convert(parameterExpression, externFeature.PropertyType), localFeature.PropertyType), typeof(object));
			func = (this.FeatureConverters[localFeature] = Expression.Lambda<Func<object, object>>(expression, new ParameterExpression[] { parameterExpression }).Compile());
			return func(externValue);
		}

		// Token: 0x040009F0 RID: 2544
		private ConcurrentDictionary<FeatureInfo, Func<object, object>> _featureConverters;
	}
}
