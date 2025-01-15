using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000077 RID: 119
	internal abstract class KeySerializer
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x000121EA File Offset: 0x000103EA
		internal static KeySerializer Create(UrlConvention urlConvention)
		{
			if (urlConvention.GenerateKeyAsSegment)
			{
				return KeySerializer.SegmentInstance;
			}
			return KeySerializer.DefaultInstance;
		}

		// Token: 0x060004D4 RID: 1236
		internal abstract void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue);

		// Token: 0x060004D5 RID: 1237 RVA: 0x00012200 File Offset: 0x00010400
		private static string GetKeyValueAsString<TProperty>(Func<TProperty, object> getPropertyValue, TProperty property, LiteralFormatter literalFormatter)
		{
			object obj = getPropertyValue.Invoke(property);
			return literalFormatter.Format(obj);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00012220 File Offset: 0x00010420
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

		// Token: 0x04000221 RID: 545
		private static readonly KeySerializer.DefaultKeySerializer DefaultInstance = new KeySerializer.DefaultKeySerializer();

		// Token: 0x04000222 RID: 546
		private static readonly KeySerializer.SegmentKeySerializer SegmentInstance = new KeySerializer.SegmentKeySerializer();

		// Token: 0x02000078 RID: 120
		private sealed class DefaultKeySerializer : KeySerializer
		{
			// Token: 0x060004D9 RID: 1241 RVA: 0x000122E2 File Offset: 0x000104E2
			internal DefaultKeySerializer()
			{
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x000122EA File Offset: 0x000104EA
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
			}
		}

		// Token: 0x02000079 RID: 121
		private sealed class SegmentKeySerializer : KeySerializer
		{
			// Token: 0x060004DB RID: 1243 RVA: 0x000122F6 File Offset: 0x000104F6
			internal SegmentKeySerializer()
			{
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x000122FE File Offset: 0x000104FE
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				if (keyProperties.Count > 1)
				{
					KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
					return;
				}
				KeySerializer.SegmentKeySerializer.AppendKeyWithSegments<TProperty>(builder, keyProperties, getPropertyValue);
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x0001231D File Offset: 0x0001051D
			private static void AppendKeyWithSegments<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, object> getPropertyValue)
			{
				builder.Append('/');
				builder.Append(KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, Enumerable.Single<TProperty>(keyProperties), LiteralFormatter.ForKeys(true)));
			}
		}
	}
}
