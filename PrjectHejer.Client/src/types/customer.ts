export interface Customer {
  customerId: string;
  name: string;
  email: string;
  phone: string;
}

export interface SingleCustomer {
  CustomerId: string;
  Name: string;
  Email: string;
  Phone: string;
  Images: Array<{ id: string; base64Image: string }>;
}
