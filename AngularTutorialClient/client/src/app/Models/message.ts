export class Message {
  id: number;
  senderId: number;
  serderUsername: string;
  senderPhotoId: string;
  recipientId: number;
  recipientUsername: string;
  recipientPhotoUrl: string;
  content: string;
  dateRead?: Date;
  messageSent: Date;
}
