using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000394 RID: 916
	public class SinkParameterValidator
	{
		// Token: 0x06001C37 RID: 7223 RVA: 0x0006B770 File Offset: 0x00069970
		public SinkParameterValidator()
		{
			this.m_exp = new Dictionary<string, Pair<bool, SinkParameterValidationFunction>>();
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0006B783 File Offset: 0x00069983
		public void Add(string name, bool mandatory)
		{
			this.Add(name, mandatory, new SinkParameterValidationFunction(this.EmptyValidator));
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0006B799 File Offset: 0x00069999
		public void Add(string name, bool mandatory, SinkParameterValidationFunction validator)
		{
			this.m_exp[name] = new Pair<bool, SinkParameterValidationFunction>(mandatory, validator);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0006B7B0 File Offset: 0x000699B0
		public void Validate(SinkIdentifier sid)
		{
			foreach (KeyValuePair<string, string> keyValuePair in sid.Parameters)
			{
				Pair<bool, SinkParameterValidationFunction> pair = null;
				if (!this.m_exp.TryGetValue(keyValuePair.Key, out pair))
				{
					string text = string.Format(CultureInfo.CurrentCulture, "parameter: '{0}' is not expected by sink '{1}'", new object[] { keyValuePair.Key, sid.SinkType });
					throw new InvalidSinkParameterException(sid, text);
				}
				if (!pair.Second(keyValuePair.Value))
				{
					string text2 = string.Format(CultureInfo.CurrentCulture, "invalid parameter: '{0}' passed to sink '{1}'", new object[] { keyValuePair.Key, sid.SinkType });
					throw new InvalidSinkParameterException(sid, text2);
				}
			}
			foreach (KeyValuePair<string, Pair<bool, SinkParameterValidationFunction>> keyValuePair2 in this.m_exp)
			{
				if (keyValuePair2.Value.First && !sid.Parameters.Has(keyValuePair2.Key))
				{
					string text3 = string.Format(CultureInfo.CurrentCulture, "Missing mandatory parameter: '{0}' for sink '{1}'", new object[] { keyValuePair2.Key, sid.SinkType });
					throw new InvalidSinkParameterException(sid, text3);
				}
			}
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000034FD File Offset: 0x000016FD
		private bool EmptyValidator(string value)
		{
			return true;
		}

		// Token: 0x04000981 RID: 2433
		private Dictionary<string, Pair<bool, SinkParameterValidationFunction>> m_exp;
	}
}
