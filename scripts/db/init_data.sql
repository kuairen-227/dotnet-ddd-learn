/*
customers
*/
INSERT INTO public.customers (id, name, email) VALUES
('7d2fd5bd-0e5a-4deb-bc82-cd6b56501b34', '佐藤 太郎', 'taro-sato@example.com'),
('8ff849d4-53b0-486a-a5dc-d4d3a66ca3bf', '鈴木 花子', 'hanako-suzuki@example.com')
;

/*
products
*/
INSERT INTO public.products (id, name, price) VALUES
('b5be9fbd-bb0e-4220-925d-b33d612790f6', 'ノートパソコン', 150000),
('248b101c-3443-4e12-8a50-7a80408af0bf', 'スマートフォン', 80000)
;

/*
orders
*/
INSERT INTO orders (id, customer_id, order_date) VALUES
('bf305a38-adb7-446b-b12d-c5460b64bb40', '7d2fd5bd-0e5a-4deb-bc82-cd6b56501b34', NOW()),
('bedc58fa-c629-41e5-9cda-f346deae47dd', '8ff849d4-53b0-486a-a5dc-d4d3a66ca3bf', NOW())
;

/*
order_items
*/
INSERT INTO order_items (id, order_id, product_id, quantity) VALUES
('1835f134-1f1d-420f-87a2-32f94d778498', 'bf305a38-adb7-446b-b12d-c5460b64bb40', 'b5be9fbd-bb0e-4220-925d-b33d612790f6', 1),
('96b9d0d1-092a-4349-b4fd-3f62ec8c29b6', 'bedc58fa-c629-41e5-9cda-f346deae47dd', '248b101c-3443-4e12-8a50-7a80408af0bf', 2)
;