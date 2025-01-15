using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000223 RID: 547
	internal abstract class KeySerializer
	{
		// Token: 0x0600162B RID: 5675 RVA: 0x00044736 File Offset: 0x00042936
		internal static KeySerializer Create(bool enableKeyAsSegment)
		{
			if (enableKeyAsSegment)
			{
				return KeySerializer.SegmentInstance;
			}
			return KeySerializer.DefaultInstance;
		}

		// Token: 0x0600162C RID: 5676
		internal abstract void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue);

		// Token: 0x0600162D RID: 5677 RVA: 0x00044748 File Offset: 0x00042948
		private static string GetKeyValueAsString<TProperty>(Func<TProperty, object> getPropertyValue, TProperty property, LiteralFormatter literalFormatter)
		{
			object obj = getPropertyValue.Invoke(property);
			return literalFormatter.Format(obj);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00044768 File Offset: 0x00042968
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
					builder.Append(getPropertyName.Invoke(tproperty));
					builder.Append('=');
				}
				string keyValueAsString = KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, tproperty, literalFormatter);
				builder.Append(keyValueAsString);
			}
			builder.Append(')');
		}

		// Token: 0x04000A4B RID: 2635
		private static readonly KeySerializer.DefaultKeySerializer DefaultInstance = new KeySerializer.DefaultKeySerializer();

		// Token: 0x04000A4C RID: 2636
		private static readonly KeySerializer.SegmentKeySerializer SegmentInstance = new KeySerializer.SegmentKeySerializer();

		// Token: 0x02000366 RID: 870
		private sealed class DefaultKeySerializer : KeySerializer
		{
			// Token: 0x06001B49 RID: 6985 RVA: 0x0004CFCD File Offset: 0x0004B1CD
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
			}
		}

		// Token: 0x02000367 RID: 871
		private sealed class SegmentKeySerializer : KeySerializer
		{
			// Token: 0x06001B4B RID: 6987 RVA: 0x0004CFD9 File Offset: 0x0004B1D9
			internal SegmentKeySerializer()
			{
			}

			// Token: 0x06001B4C RID: 6988 RVA: 0x0004CFE1 File Offset: 0x0004B1E1
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				if (keyProperties.Count > 1)
				{
					KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
					return;
				}
				KeySerializer.SegmentKeySerializer.AppendKeyWithSegments<TProperty>(builder, keyProperties, getPropertyValue);
			}

			// Token: 0x06001B4D RID: 6989 RVA: 0x0004D000 File Offset: 0x0004B200
			private static void AppendKeyWithSegments<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, object> getPropertyValue)
			{
				builder.Append('/');
				builder.Append(KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, Enumerable.Single<TProperty>(keyProperties), LiteralFormatter.ForKeys(true)));
			}
		}
	}
}
