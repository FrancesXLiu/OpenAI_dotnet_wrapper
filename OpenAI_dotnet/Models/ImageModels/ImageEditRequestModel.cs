using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_dotnet;

public class ImageEditRequestModel : ImageCreateRequestModel
{
    /// <summary>
    /// The image to edit. Must be a valid PNG file, less than 4MB, and square. If mask is not provided, image must have transparency, which will be used as the mask.
    /// </summary>
    public byte[] Image { get; set; }
    public string ImageName { get; set; }

    /// <summary>
    /// An additional image whose fully transparent areas (e.g. where alpha is zero) indicate where image should be edited. Must be a valid PNG file, less than 4MB, and have the same dimensions as image.
    /// </summary>
    public byte[]? Mask { get; set; }
    public string? MaskName { get; set; }
}
