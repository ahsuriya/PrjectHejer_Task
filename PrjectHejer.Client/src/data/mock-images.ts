export const mockImages = Array.from({ length: 10 }).map((_, index) => ({
  id: index + 1,
  base64Image: `/image-${index + 1}.jpg`,
}));
