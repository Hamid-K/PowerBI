using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Client
{
	// Token: 0x0200000E RID: 14
	internal abstract class KeySerializer
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000037AE File Offset: 0x000019AE
		internal static KeySerializer Create(bool enableKeyAsSegment)
		{
			if (enableKeyAsSegment)
			{
				return KeySerializer.SegmentInstance;
			}
			return KeySerializer.DefaultInstance;
		}

		// Token: 0x0600005E RID: 94
		internal abstract void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue);

		// Token: 0x0600005F RID: 95 RVA: 0x000037C0 File Offset: 0x000019C0
		private static string GetKeyValueAsString<TProperty>(Func<TProperty, object> getPropertyValue, TProperty property, LiteralFormatter literalFormatter)
		{
			object obj = getPropertyValue(property);
			return literalFormatter.Format(obj);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000037E0 File Offset: 0x000019E0
		private static void AppendKeyWithParentheses<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
		{
			LiteralFormatter literalFormatter = LiteralFormatter.ForKeys(false);
			builder.Append('(');
			bool flag = true;
			foreach (TProperty tproperty in keyProperties)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					builder.Append(',');
				}
				if (keyProperties.Count != 1)
				{
					builder.Append(getPropertyName(tproperty));
					builder.Append('=');
				}
				string keyValueAsString = KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, tproperty, literalFormatter);
				builder.Append(keyValueAsString);
			}
			builder.Append(')');
		}

		// Token: 0x04000027 RID: 39
		private static readonly KeySerializer.DefaultKeySerializer DefaultInstance = new KeySerializer.DefaultKeySerializer();

		// Token: 0x04000028 RID: 40
		private static readonly KeySerializer.SegmentKeySerializer SegmentInstance = new KeySerializer.SegmentKeySerializer();

		// Token: 0x02000144 RID: 324
		private sealed class DefaultKeySerializer : KeySerializer
		{
			// Token: 0x06000CE4 RID: 3300 RVA: 0x0002D819 File Offset: 0x0002BA19
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
			}
		}

		// Token: 0x02000145 RID: 325
		private sealed class SegmentKeySerializer : KeySerializer
		{
			// Token: 0x06000CE6 RID: 3302 RVA: 0x0002D825 File Offset: 0x0002BA25
			internal SegmentKeySerializer()
			{
			}

			// Token: 0x06000CE7 RID: 3303 RVA: 0x0002D82D File Offset: 0x0002BA2D
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				if (keyProperties.Count > 1)
				{
					KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
					return;
				}
				KeySerializer.SegmentKeySerializer.AppendKeyWithSegments<TProperty>(builder, keyProperties, getPropertyValue);
			}

			// Token: 0x06000CE8 RID: 3304 RVA: 0x0002D84C File Offset: 0x0002BA4C
			private static void AppendKeyWithSegments<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, object> getPropertyValue)
			{
				builder.Append('/');
				builder.Append(KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, keyProperties.Single<TProperty>(), LiteralFormatter.ForKeys(true)));
			}
		}
	}
}
