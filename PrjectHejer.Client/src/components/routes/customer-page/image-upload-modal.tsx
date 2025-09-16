"use client";

import { Modal } from "@/components/shared/modal";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { useImageUploadModal } from "@/hooks/use-image-upload-modal";
import { api } from "@/lib/api";
import { fileToBase64 } from "@/lib/file";
import { zodResolver } from "@hookform/resolvers/zod";
import { UploadIcon } from "lucide-react";
import { useParams, useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "sonner";
import * as z from "zod";
import { ImageUploader } from "./image-uploader";

const formSchema = z.object({
  images: z
    .array(z.instanceof(File))
    .min(1, "At least one image is required")
    .max(10, "You can upload up to 10 images"),
});

type FormSchema = z.infer<typeof formSchema>;

export const ImageUploadModal = ({ fileLimit }: { fileLimit: number }) => {
  const [isLoading, setIsLoading] = useState(false);
  const router = useRouter();
  const params = useParams();

  const form = useForm<FormSchema>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      images: [],
    },
  });
  const imageUploadModal = useImageUploadModal();

  const onSubmit = async (data: FormSchema) => {
    setIsLoading(true);
    try {
      const base64Files = await Promise.all(data.images.map(fileToBase64));
      const response = await api.post(
        `/CustomerImages/${params.customerId}`,
        base64Files
      );

      if (response.data.data.Success) {
        toast.success("Images uploaded successfully");
        form.reset();
        router.refresh();
        imageUploadModal.onClose();
      } else {
        toast.error("Failed to upload images. Please try again.");
      }
    } catch (error) {
      console.error("Upload failed:", error);
      toast.error("Failed to upload images. Please try again.");
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (!imageUploadModal.isOpen) {
      form.reset();
    }
  }, [imageUploadModal.isOpen, form]);

  return (
    <Modal
      title="Upload Images"
      description="Add images to customer's profile"
      isOpen={imageUploadModal.isOpen}
      onClose={imageUploadModal.onClose}
      className="!max-w-5xl !max-h-[90dvh]"
    >
      <div className="py-2 pb-4 space-y-4 h-full">
        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(onSubmit)}
            className="flex flex-col gap-y-4 h-full"
          >
            <FormField
              name="images"
              control={form.control}
              render={({ field }) => (
                <FormItem className="grow">
                  <FormControl>
                    <ImageUploader
                      onChange={field.onChange}
                      disabled={isLoading}
                      maxFiles={fileLimit}
                    />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <div className="flex items-center justify-end w-full pt-6 space-x-2">
              <Button
                disabled={isLoading}
                variant="outline"
                type="button"
                onClick={imageUploadModal.onClose}
              >
                Cancel
              </Button>
              <Button disabled={isLoading} type="submit">
                <UploadIcon />
                {isLoading ? "Uploading..." : "Upload"}
              </Button>
            </div>
          </form>
        </Form>
      </div>
    </Modal>
  );
};
