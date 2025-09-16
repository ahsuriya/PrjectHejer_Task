"use client";

import { Heading } from "@/components/shared/heading";
import { Button } from "@/components/ui/button";
import { Card, CardContent } from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";
import { useCarouselModal } from "@/hooks/use-carousel-modal";
import { useImageUploadModal } from "@/hooks/use-image-upload-modal";
import { api } from "@/lib/api";
import { SingleCustomer } from "@/types/customer";
import { GalleryHorizontalIcon, TrashIcon, UploadIcon } from "lucide-react";
import Image from "next/image";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { toast } from "sonner";
import { CarouselModal } from "./carousel-modal";
import { AlertModal } from "./delete-modal";
import { ImageUploadModal } from "./image-upload-modal";

export const CustomerPageClient = ({
  customer,
}: {
  customer: SingleCustomer;
}) => {
  const imageUploadModal = useImageUploadModal();
  const carouselModal = useCarouselModal();
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [openDeleteModal, setOpenDeleteModal] = useState(false);
  const [loading, setLoading] = useState(false);
  const router = useRouter();

  const onDelete = async (id: string) => {
    try {
      setLoading(true);
      const response = await api.delete(`/CustomerImages/${id}`);

      if (response.data == "") {
        toast.success("Image deleted successfully");
        setOpenDeleteModal(false);
        setDeleteId(null);
        router.refresh();
      } else {
        toast.error("Failed to delete image. Please try again.");
      }
    } catch (error) {
      console.error("Error deleting image:", error);
      toast.error("Failed to delete image. Please try again.");
    } finally {
      setLoading(false);
      // setOpenDeleteModal(false);
      // setDeleteId(null);
    }
  };

  return (
    <>
      <div className="flex items center justify-between">
        <Heading
          title={`${customer.Name}'s Details`}
          description={customer.Email}
        />
        <div className="flex flex-col gap-2">
          <Button size={"sm"} onClick={carouselModal.onOpen}>
            <GalleryHorizontalIcon />
            View Carousel
          </Button>
          <Button
            size={"sm"}
            variant="secondary"
            onClick={imageUploadModal.onOpen}
          >
            <UploadIcon />
            Upload Images
          </Button>
        </div>
      </div>
      <Separator />
      <div className="grid grid-cols-5 max-xl:grid-cols-4 max-lg:grid-cols-3 max-md:grid-cols-2 max-sm:grid-cols-1 gap-4">
        {customer.Images.map((image) => (
          <Card key={image.id} className="overflow-hidden p-0">
            <CardContent className="relative aspect-square">
              <Button
                size="icon"
                variant={"destructive"}
                className="absolute top-2 right-2 z-10 cursor-pointer"
                onClick={() => {
                  setDeleteId(image.id);
                  setOpenDeleteModal(true);
                }}
              >
                <TrashIcon />
              </Button>
              <Image
                fill
                src={`data:image/jpeg;base64,${image.base64Image}`}
                alt={`Image ${image.id}`}
                className="object-cover w-full h-full"
              />
            </CardContent>
          </Card>
        ))}
      </div>
      <ImageUploadModal fileLimit={10 - customer.Images.length} />
      <CarouselModal images={customer.Images} />
      <AlertModal
        isOpen={openDeleteModal}
        loading={loading}
        onClose={() => setOpenDeleteModal(false)}
        onConfirm={() => {
          if (deleteId) {
            onDelete(deleteId);
          }
        }}
      />
    </>
  );
};
