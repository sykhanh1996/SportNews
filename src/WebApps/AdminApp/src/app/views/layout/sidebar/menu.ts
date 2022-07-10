import { MenuItem } from './menu.model';

export const MENU: MenuItem[] = [
  {
    label: 'Main',
    isTitle: true
  },
  {
    label: 'Trang chủ',
    icon: 'home',
    link: '/dashboard'
  },
  {
    label: 'Quản lý',
    isTitle: true
  },
  {
    label: 'Bài viết',
    icon: 'book-open',
    subItems: [
      {
        label: 'Bóng rổ',
        link: '/news/basketball',
      }
    ]
  }
];
