#pragma once

#include "Enumerations.h"

using namespace System::Runtime::InteropServices;

namespace Squalr
{
	namespace Engine
	{
		namespace Internal
		{
			namespace Fasm
			{
				/// <summary>
				/// The following structure has two variants - it either defines the line
				/// that was loaded directly from source, or the line that was generated by
				/// macroinstruction. First case has the highest bit of line_number set to 0,
				/// while the second case has this bit set.
				///
				/// In the first case, the file_path field contains pointer to the path of
				/// source file (empty string if it's the source that was provided directly to
				/// fasm_Assemble function), the line_number is the number of line within
				/// that file (starting from 1) and the file_offset field contains the offset
				/// within the file where the line starts.
				///
				/// In the second case the macro_calling_line field contains the pointer to
				/// LINE_HEADER structure for the line which called the macroinstruction, and
				/// the macro_line field contains the pointer to LINE_HEADER structure for the
				/// line within the definition of macroinstruction, which generated this one.
				/// </summary>
				struct NativeLineHeader
				{
					char* FilePath;
					int LineNumber;
					union
					{
						int FileOffset;
						NativeLineHeader* MacroCallingFile;
					};
					NativeLineHeader* MacroLine;
				};

				/// <summary>
				/// The following structure resides at the beginning of memory block provided
				/// to the fasm_Assemble function. The condition field contains the same value
				/// as the one returned by function.
				///
				/// When function returns FASM_OK condition, the output_length and
				/// output_data fields are filled - with pointer to generated output
				/// (somewhere within the provided memory block) and the count of bytes stored
				/// there.
				///
				/// When function returns FASM_ERROR, the error_code is filled with the
				/// code of specific error that happened and error_line is a pointer to the
				/// LINE_HEADER structure, providing information about the line that caused
				/// the error.
				/// </summary>
				struct NativeFasmState
				{
					FasmResults Condition;
					union
					{
						int OutputLength;
						int ErrorCode;
					};
					union
					{
						byte* OutputData;
						NativeLineHeader* ErrorLine;
					};
				};
			}
			//// End namespace
		}
		//// End namespace
	}
	//// End namespace
}
//// End namespace