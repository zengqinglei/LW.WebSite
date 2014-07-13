use lolitabox;
alter table box modify coupon_valid_date date null;
SET SQL_SAFE_UPDATES=0;
update box set coupon_valid_date = null where coupon_valid_date = '0000-00-00';