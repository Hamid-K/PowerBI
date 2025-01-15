using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000803 RID: 2051
	public class DrdaException : Exception
	{
		// Token: 0x060040B1 RID: 16561 RVA: 0x000DC59A File Offset: 0x000DA79A
		public DrdaException(ErrorCodePoint errCodePoint, CodePoint contextCp, params object[] args)
			: this(null, errCodePoint, contextCp, args)
		{
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x000DC5A6 File Offset: 0x000DA7A6
		public DrdaException(IExceptionHandler handler, ErrorCodePoint errCodePoint, CodePoint contextCp, params object[] args)
			: this(handler, errCodePoint, contextCp, 0, args)
		{
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x000DC5B4 File Offset: 0x000DA7B4
		public DrdaException(ErrorCodePoint errCodePoint, CodePoint contextCp, int errorCode, params object[] args)
			: this(null, errCodePoint, contextCp, errorCode, args)
		{
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x000DC5C2 File Offset: 0x000DA7C2
		public DrdaException(IExceptionHandler handler, ErrorCodePoint errCodePoint, CodePoint contextCp, int errorCode, params object[] args)
		{
			this._exceptionHandler = handler;
			this._errorCodePoint = errCodePoint;
			this._contextCodePoint = contextCp;
			this._errorCode = errorCode;
			this._args = args;
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x000DC5EF File Offset: 0x000DA7EF
		// (set) Token: 0x060040B6 RID: 16566 RVA: 0x000DC5F6 File Offset: 0x000DA7F6
		public Dictionary<ErrorCodePoint, DrdaExceptionInfo> ExceptionInfomations
		{
			get
			{
				return DrdaException._exInfos;
			}
			set
			{
				DrdaException._exInfos = value;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x000DC5FE File Offset: 0x000DA7FE
		// (set) Token: 0x060040B8 RID: 16568 RVA: 0x000DC606 File Offset: 0x000DA806
		public IExceptionHandler ExceptionHandler
		{
			get
			{
				return this._exceptionHandler;
			}
			set
			{
				this._exceptionHandler = value;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x000DC60F File Offset: 0x000DA80F
		// (set) Token: 0x060040BA RID: 16570 RVA: 0x000DC617 File Offset: 0x000DA817
		public ErrorCodePoint ErrorCodePoint
		{
			get
			{
				return this._errorCodePoint;
			}
			set
			{
				this._errorCodePoint = value;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x000DC620 File Offset: 0x000DA820
		// (set) Token: 0x060040BC RID: 16572 RVA: 0x000DC628 File Offset: 0x000DA828
		public CodePoint ContextCodePoint
		{
			get
			{
				return this._contextCodePoint;
			}
			set
			{
				this._contextCodePoint = value;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x000DC631 File Offset: 0x000DA831
		// (set) Token: 0x060040BE RID: 16574 RVA: 0x000DC639 File Offset: 0x000DA839
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
			set
			{
				this._errorCode = value;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x000DC642 File Offset: 0x000DA842
		// (set) Token: 0x060040C0 RID: 16576 RVA: 0x000DC64A File Offset: 0x000DA84A
		public object Arguments
		{
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x000DC653 File Offset: 0x000DA853
		public bool IsFatal
		{
			get
			{
				return this._errorCodePoint == ErrorCodePoint.UNKNOWN || this._errorCodePoint == ErrorCodePoint.DISCONNECT || this._errorCodePoint == ErrorCodePoint.AGENTERROR;
			}
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x000DC673 File Offset: 0x000DA873
		public static void TooBig(CodePoint cp)
		{
			DrdaException.ThrowSyntaxrm(cp, SyntaxErrorCode.TooBig);
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x000DC67D File Offset: 0x000DA87D
		public static void BadObjectLength(CodePoint cp)
		{
			DrdaException.ThrowSyntaxrm(cp, SyntaxErrorCode.ObjectLengthNotAllowed);
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x000DC687 File Offset: 0x000DA887
		public static void MissingCodePoint(CodePoint cp)
		{
			DrdaException.ThrowSyntaxrm(cp, SyntaxErrorCode.RequiredObjectNotFound);
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x000DC691 File Offset: 0x000DA891
		public static void TooMany(CodePoint cp)
		{
			DrdaException.ThrowSyntaxrm(cp, SyntaxErrorCode.TooMany);
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x000DC69B File Offset: 0x000DA89B
		public static void InvalidValue(CodePoint cp)
		{
			DrdaException.ThrowSyntaxrm(cp, SyntaxErrorCode.RequiredValueNotFound);
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x000DC6A5 File Offset: 0x000DA8A5
		public static void InvalidCodePoint(CodePoint cp)
		{
			DrdaException.ThrowSyntaxrm(cp, SyntaxErrorCode.InvalidCodePoint);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x000DC6AF File Offset: 0x000DA8AF
		public static void ThrowSyntaxrm(CodePoint cp, SyntaxErrorCode errorCode)
		{
			throw new DrdaException(ErrorCodePoint.SYNTAXRM, cp, (int)errorCode, Array.Empty<object>());
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x000DC6C2 File Offset: 0x000DA8C2
		public static void ThrowSyntaxrm(SyntaxErrorCode errorCode)
		{
			throw new DrdaException(ErrorCodePoint.SYNTAXRM, CodePoint.UNKNOWN, (int)errorCode, Array.Empty<object>());
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x000DC6D5 File Offset: 0x000DA8D5
		public static void RdbNotFound(CodePoint cp, string name)
		{
			throw new DrdaException(ErrorCodePoint.RDBNFNRM, cp, new object[] { name });
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x000DC6EC File Offset: 0x000DA8EC
		public static void RdbnamMismatch(CodePoint cp, string name)
		{
			DrdaException.RdbnamMismatch(null, cp, name);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x000DC6F6 File Offset: 0x000DA8F6
		public static void RdbnamMismatch(IExceptionHandler handler, CodePoint cp, string name)
		{
			throw new DrdaException(handler, ErrorCodePoint.PRCCNVRM, cp, 18, new object[] { name });
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x000DC710 File Offset: 0x000DA910
		public static void CommunicationFailure(Exception ex)
		{
			DrdaException.CommunicationFailure(ex.Message);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x000DC71D File Offset: 0x000DA91D
		public static void CommunicationFailure(string message)
		{
			throw new DrdaException(ErrorCodePoint.DISCONNECT, CodePoint.UNKNOWN, new object[] { message });
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x000DC731 File Offset: 0x000DA931
		public static void ParameterValueNotSupported(CodePoint cp)
		{
			throw new DrdaException(ErrorCodePoint.VALNSPRM, cp, Array.Empty<object>());
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x000DC731 File Offset: 0x000DA931
		public static void ValueNotSupported(CodePoint cp)
		{
			throw new DrdaException(ErrorCodePoint.VALNSPRM, cp, Array.Empty<object>());
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x000DC744 File Offset: 0x000DA944
		static DrdaException()
		{
			DrdaException._exInfos.Add(ErrorCodePoint.CMDCHKRM, new DrdaExceptionInfo(ErrorCodePoint.CMDCHKRM, 8, CodePoint.UNKNOWN, false));
			DrdaException._exInfos.Add(ErrorCodePoint.CMDNSPRM, new DrdaExceptionInfo(ErrorCodePoint.CMDNSPRM, 8, CodePoint.UNKNOWN, true));
			DrdaException._exInfos.Add(ErrorCodePoint.DTAMCHRM, new DrdaExceptionInfo(ErrorCodePoint.DTAMCHRM, 8, CodePoint.UNKNOWN, false));
			DrdaException._exInfos.Add(ErrorCodePoint.OBJNSPRM, new DrdaExceptionInfo(ErrorCodePoint.OBJNSPRM, 8, CodePoint.UNKNOWN, true));
			DrdaException._exInfos.Add(ErrorCodePoint.PKGBNARM, new DrdaExceptionInfo(ErrorCodePoint.PKGBNARM, 8, CodePoint.UNKNOWN, false));
			DrdaException._exInfos.Add(ErrorCodePoint.PRCCNVRM, new DrdaExceptionInfo(ErrorCodePoint.PRCCNVRM, 8, CodePoint.PRCCNVCD, false));
			DrdaException._exInfos.Add(ErrorCodePoint.SYNTAXRM, new DrdaExceptionInfo(ErrorCodePoint.SYNTAXRM, 8, CodePoint.SYNERRCD, true));
			DrdaException._exInfos.Add(ErrorCodePoint.VALNSPRM, new DrdaExceptionInfo(ErrorCodePoint.VALNSPRM, 8, CodePoint.UNKNOWN, true));
			DrdaException._exInfos.Add(ErrorCodePoint.MGRLVLRM, new DrdaExceptionInfo(ErrorCodePoint.MGRLVLRM, 8, CodePoint.UNKNOWN, false));
			DrdaException._exInfos.Add(ErrorCodePoint.RDBNFNRM, new DrdaExceptionInfo(ErrorCodePoint.RDBNFNRM, 8, CodePoint.UNKNOWN, false));
			DrdaException._exInfos.Add(ErrorCodePoint.SECCHKRM, new DrdaExceptionInfo(ErrorCodePoint.SECCHKRM, 8, CodePoint.UNKNOWN, false));
			DrdaException._exInfos.Add(ErrorCodePoint.DISCONNECT, new DrdaExceptionInfo(ErrorCodePoint.DISCONNECT, 0, CodePoint.UNKNOWN, false));
			DrdaException._exInfos.Add(ErrorCodePoint.AGENTERROR, new DrdaExceptionInfo(ErrorCodePoint.AGENTERROR, 0, CodePoint.UNKNOWN, false));
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x000DC8C4 File Offset: 0x000DAAC4
		public void BeginWrite(DdmWriter writer)
		{
			writer.CreateDssReply();
			writer.WriteBeginDdm((CodePoint)this._errorCodePoint);
			writer.WriteScalar2Bytes(CodePoint.SVRCOD, this._errorCode);
			DrdaExceptionInfo drdaExceptionInfo = DrdaException._exInfos[this._errorCodePoint];
			if (drdaExceptionInfo != null)
			{
				if (drdaExceptionInfo.SendCodePointArgument)
				{
					writer.WriteScalar2Bytes(CodePoint.CODPNT, (int)drdaExceptionInfo.CodePointArgument);
				}
				if (drdaExceptionInfo.CodePoint != ErrorCodePoint.UNKNOWN)
				{
					writer.WriteScalar1Byte((CodePoint)drdaExceptionInfo.CodePoint, (int)this._errorCodePoint);
				}
			}
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x000DC938 File Offset: 0x000DAB38
		public void EndWrite(DdmWriter writer)
		{
			writer.WriteEndDdm();
			writer.WriteEndDss();
		}

		// Token: 0x04002D9D RID: 11677
		private static Dictionary<ErrorCodePoint, DrdaExceptionInfo> _exInfos = new Dictionary<ErrorCodePoint, DrdaExceptionInfo>();

		// Token: 0x04002D9E RID: 11678
		private IExceptionHandler _exceptionHandler;

		// Token: 0x04002D9F RID: 11679
		private ErrorCodePoint _errorCodePoint;

		// Token: 0x04002DA0 RID: 11680
		private CodePoint _contextCodePoint;

		// Token: 0x04002DA1 RID: 11681
		private int _errorCode;

		// Token: 0x04002DA2 RID: 11682
		private object _args;
	}
}
